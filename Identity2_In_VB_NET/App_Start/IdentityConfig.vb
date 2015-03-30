Imports System.Threading.Tasks
Imports System.Security.Claims
Imports Microsoft.AspNet.Identity
Imports Microsoft.AspNet.Identity.EntityFramework
Imports Microsoft.AspNet.Identity.Owin
Imports Microsoft.Owin
Imports Microsoft.Owin.Security
Imports System.Data.Entity

Public Class EmailService
    Implements IIdentityMessageService

    Public Function SendAsync(message As IdentityMessage) As Task Implements IIdentityMessageService.SendAsync
        ' Plug in your email service here to send an email.
        Return Task.FromResult(0)
    End Function
End Class

Public Class SmsService
    Implements IIdentityMessageService

    Public Function SendAsync(message As IdentityMessage) As Task Implements IIdentityMessageService.SendAsync
        ' Plug in your SMS service here to send a text message.
        Return Task.FromResult(0)
    End Function
End Class

' Configure the application user manager used in this application. UserManager is defined in ASP.NET Identity and is used by the application.
Public Class ApplicationUserManager
    Inherits UserManager(Of ApplicationUser)

    Public Sub New(store As IUserStore(Of ApplicationUser))
        MyBase.New(store)
    End Sub

    Public Shared Function Create(options As IdentityFactoryOptions(Of ApplicationUserManager), context As IOwinContext)
        Dim manager = New ApplicationUserManager(New UserStore(Of ApplicationUser)(context.Get(Of ApplicationDbContext)()))

        ' Configure validation logic for usernames
        manager.UserValidator = New UserValidator(Of ApplicationUser)(manager) With {
            .AllowOnlyAlphanumericUserNames = False,
            .RequireUniqueEmail = True
        }

        ' Configure validation logic for passwords
        manager.PasswordValidator = New PasswordValidator With {
            .RequiredLength = 6,
            .RequireNonLetterOrDigit = True,
            .RequireDigit = True,
            .RequireLowercase = True,
            .RequireUppercase = True
        }

        ' Configure user lockout defaults
        manager.UserLockoutEnabledByDefault = True
        manager.DefaultAccountLockoutTimeSpan = TimeSpan.FromMinutes(5)
        manager.MaxFailedAccessAttemptsBeforeLockout = 5

        ' Register two factor authentication providers. This application uses Phone and Emails as a step of receiving a code for verifying the user
        ' You can write your own provider and plug it in here.
        manager.RegisterTwoFactorProvider("Phone Code", New PhoneNumberTokenProvider(Of ApplicationUser) With {
                                          .MessageFormat = "Your security code is {0}"
                                      })
        manager.RegisterTwoFactorProvider("Email Code", New EmailTokenProvider(Of ApplicationUser) With {
                                          .Subject = "Security Code",
                                          .BodyFormat = "Your security code is {0}"
                                          })
        manager.EmailService = New EmailService()
        manager.SmsService = New SmsService()
        Dim dataProtectionProvider = options.DataProtectionProvider
        If (dataProtectionProvider IsNot Nothing) Then
            manager.UserTokenProvider = New DataProtectorTokenProvider(Of ApplicationUser)(dataProtectionProvider.Create("ASP.NET Identity"))
        End If

        Return manager
    End Function

End Class

' Configure the application sign-in manager which is used in this application.
Public Class ApplicationSignInManager
    Inherits SignInManager(Of ApplicationUser, String)
    Public Sub New(userManager As ApplicationUserManager, authenticationManager As IAuthenticationManager)
        MyBase.New(userManager, authenticationManager)
    End Sub

    Public Overrides Function CreateUserIdentityAsync(user As ApplicationUser) As Task(Of ClaimsIdentity)
        Return user.GenerateUserIdentityAsync(DirectCast(UserManager, ApplicationUserManager))
    End Function

    Public Shared Function Create(options As IdentityFactoryOptions(Of ApplicationSignInManager), context As IOwinContext) As ApplicationSignInManager
        Return New ApplicationSignInManager(context.GetUserManager(Of ApplicationUserManager)(), context.Authentication)
    End Function
End Class


'Configure the application Role Manager wich is used in this application

Public Class ApplicationRoleManager
    Inherits RoleManager(Of IdentityRole)
    Public Sub New(store As IRoleStore(Of IdentityRole, String))
        MyBase.New(store)
    End Sub



    Public Shared Function Create(options As IdentityFactoryOptions(Of ApplicationRoleManager), context As IOwinContext) As ApplicationRoleManager
        Dim roleStore = New RoleStore(Of IdentityRole)(context.[Get](Of ApplicationDbContext)())
        Return New ApplicationRoleManager(roleStore)
    End Function
End Class


Public Class ApplicationDbInitializer
    Inherits DropCreateDatabaseIfModelChanges(Of ApplicationDbContext)
    Protected Overrides Sub Seed(context As ApplicationDbContext)
        InitializeIdentityForEF(context)
        MyBase.Seed(context)
    End Sub

    'Create User=Admin@Admin.com with password=Admin@123456 in the Admin role        
    Public Shared Sub InitializeIdentityForEF(db As ApplicationDbContext)
        Dim userManager = HttpContext.Current.GetOwinContext().GetUserManager(Of ApplicationUserManager)()
        Dim roleManager = HttpContext.Current.GetOwinContext().[Get](Of ApplicationRoleManager)()
        Const name As String = "admin@example.com"
        Const password As String = "Admin@123456"
        Const roleName As String = "Admin"

        'Create Role Admin if it does not exist
        Dim role = roleManager.FindByName(roleName)
        If role Is Nothing Then
            role = New ApplicationRole(roleName)
            Dim roleresult = roleManager.Create(role)
        End If

        Dim user = userManager.FindByName(name)
        If user Is Nothing Then
            user = New ApplicationUser() With { _
                 .UserName = name, _
                 .Email = name _
            }
            Dim result = userManager.Create(user, password)
            result = userManager.SetLockoutEnabled(user.Id, False)
        End If

        ' Add user admin to Role Admin if not already added
        Dim rolesForUser = userManager.GetRoles(user.Id)
        If Not rolesForUser.Contains(role.Name) Then
            Dim result = userManager.AddToRole(user.Id, role.Name)
        End If
    End Sub
End Class
