Imports System.Collections.Generic
Imports System.ComponentModel.DataAnnotations
Imports System.Web.Mvc

Namespace Models


    Public Class RoleViewModel
        Public Property id As String
        <Required(allowemptystrings:=False)>
        <Display(name:="Role Name")>
        Public Property name As String

    End Class

    Public Class EditUserViewModel
        Public Property Id() As String


        <Required(AllowEmptyStrings:=False)>
        <Display(Name:="Email")>
        <EmailAddress>
        Public Property Email() As String

        Public Property RolesList() As IEnumerable(Of SelectListItem)
    End Class
End Namespace