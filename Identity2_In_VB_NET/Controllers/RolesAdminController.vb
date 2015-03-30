Imports System.Web.Mvc
Imports Microsoft.AspNet.Identity
Imports Microsoft.AspNet.Identity.Owin
Imports Microsoft.AspNet.Identity.EntityFramework
Imports System.Linq
Imports System.Net
Imports System.Threading.Tasks
Imports System.Web
Imports System.Collections.Generic
Imports Identity2_In_VB_NET.Models

Namespace Controllers

    <Authorize(Roles:="Admin")> _
    Public Class RolesAdminController
        Inherits Controller
        Public Sub New()
        End Sub

        Public Sub New(userMngr As ApplicationUserManager, roleMngr As ApplicationRoleManager)
            UserManager = userMngr
            RoleManager = roleMngr
        End Sub
        Private _roleManager As ApplicationRoleManager
        Private _userManager As ApplicationUserManager
        Public Property UserManager() As ApplicationUserManager
            Get
                Return If(_userManager, HttpContext.GetOwinContext().GetUserManager(Of ApplicationUserManager)())
            End Get
            Set(value As ApplicationUserManager)
                _userManager = value
            End Set
        End Property


        Public Property RoleManager() As ApplicationRoleManager
            Get
                Return If(_roleManager, HttpContext.GetOwinContext().[Get](Of ApplicationRoleManager)())
            End Get
            Private Set(value As ApplicationRoleManager)
                _roleManager = value
            End Set
        End Property

        '
        ' GET: /Roles/
        Public Function Index() As ActionResult
            Return View(RoleManager.Roles)
        End Function

        '
        ' GET: /Roles/Details/5
        Public Async Function Details(id As String) As Task(Of ActionResult)
            If id Is Nothing Then
                Return New HttpStatusCodeResult(HttpStatusCode.BadRequest)
            End If
            Dim role = Await RoleManager.FindByIdAsync(id)
            ' Get the list of Users in this Role
            Dim users = New List(Of ApplicationUser)()

            ' Get the list of Users in this Role
            For Each user_item In UserManager.Users.ToList()
                If Await UserManager.IsInRoleAsync(user_item.Id, role.Name) Then
                    users.Add(user_item)
                End If
            Next

            ViewBag.Users = users
            ViewBag.UserCount = users.Count()
            Return View(role)
        End Function

        '
        ' GET: /Roles/Create
        Public Function Create() As ActionResult
            Return View()
        End Function

        '
        ' POST: /Roles/Create
        <HttpPost> _
        Public Async Function Create(roleViewModel As RoleViewModel) As Task(Of ActionResult)
            If ModelState.IsValid Then
                Dim role = New ApplicationRole(roleViewModel.name)

                ' Save the new Description property:

                Dim roleresult = Await RoleManager.CreateAsync(role)
                If Not roleresult.Succeeded Then
                    ModelState.AddModelError("", roleresult.Errors.First())
                    Return View()
                End If
                Return RedirectToAction("Index")
            End If
            Return View()
        End Function

        '
        ' GET: /Roles/Edit/Admin
        Public Async Function Edit(id As String) As Task(Of ActionResult)
            If id Is Nothing Then
                Return New HttpStatusCodeResult(HttpStatusCode.BadRequest)
            End If
            Dim role = Await RoleManager.FindByIdAsync(id)
            If role Is Nothing Then
                Return HttpNotFound()
            End If
            Dim roleModel As New RoleViewModel() With { _
                 .id = role.Id, _
                 .name = role.Name _
            }


            Return View(roleModel)
        End Function


        <HttpPost> _
        <ValidateAntiForgeryToken> _
        Public Async Function Edit(<Bind(Include:="Name,Id")> roleModel As RoleViewModel) As Task(Of ActionResult)
            If ModelState.IsValid Then
                Dim role = Await RoleManager.FindByIdAsync(roleModel.id)
                role.Name = roleModel.name


                Await RoleManager.UpdateAsync(role)
                Return RedirectToAction("Index")
            End If
            Return View()
        End Function

        '
        ' GET: /Roles/Delete/5
        Public Async Function Delete(id As String) As Task(Of ActionResult)
            If id Is Nothing Then
                Return New HttpStatusCodeResult(HttpStatusCode.BadRequest)
            End If
            Dim role = Await RoleManager.FindByIdAsync(id)
            If role Is Nothing Then
                Return HttpNotFound()
            End If
            Return View(role)
        End Function

        '
        ' POST: /Roles/Delete/5
        <HttpPost, ActionName("Delete")> _
        <ValidateAntiForgeryToken> _
        Public Async Function DeleteConfirmed(id As String, deleteUser As String) As Task(Of ActionResult)
            If ModelState.IsValid Then
                If id Is Nothing Then
                    Return New HttpStatusCodeResult(HttpStatusCode.BadRequest)
                End If
                Dim role = Await RoleManager.FindByIdAsync(id)
                If role Is Nothing Then
                    Return HttpNotFound()
                End If
                Dim result As IdentityResult
                If deleteUser IsNot Nothing Then
                    result = Await RoleManager.DeleteAsync(role)
                Else
                    result = Await RoleManager.DeleteAsync(role)
                End If
                If Not result.Succeeded Then
                    ModelState.AddModelError("", result.Errors.First())
                    Return View()
                End If
                Return RedirectToAction("Index")
            End If
            Return View()
        End Function
    End Class
End Namespace