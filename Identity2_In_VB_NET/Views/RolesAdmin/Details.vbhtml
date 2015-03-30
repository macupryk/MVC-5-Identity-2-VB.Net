@Modeltype Identity2_In_VB_NET.ApplicationRole
@Code
    ViewData("Title") = "Details"
End Code

<h2>Role Details.</h2>
<div>
   
    <hr />
    <dl class="dl-horizontal">
        <dt>
            @Html.DisplayNameFor(Function(Model) Model.Name)
        </dt>
        <dd>
            @Html.DisplayFor(Function(Model) Model.Name)
        </dd>
    </dl>
    
</div>
<h4>List of users in this role</h4>
@If (ViewBag.UserCount = 0) Then

    @<hr />
    @<p> No users found in this role.</p>
End If

<table class="table">

    @For Each item In ViewBag.Users
           
        @<tr>
            <td>
                @item.UserName
            </td>
        </tr>
    Next
</table>
<p>
    @Html.ActionLink("Edit", "Edit", New With {.id = Model.Id}) |
    @Html.ActionLink("Back to List", "Index")
</p>

