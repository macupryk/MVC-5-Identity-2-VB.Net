@ModelType Identity2_In_VB_NET.ApplicationUser
@Code
    ViewData("Title") = "Details"
End Code

<h2>User Details</h2>

<div>
    <h4>User</h4>
    <hr />
    <dl class="dl-horizontal">
        <dt>
            @Html.DisplayNameFor(Function(model) model.Email)
        </dt>

        <dd>
            @Html.DisplayFor(Function(model) model.Email)
        </dd>

    </dl>
</div>
<h4>List of roles for this user</h4>
@If ViewBag.rolenames.count = 0 Then
     @<hr />
    @<p>No roles found for this user.</p>
End If
<table class="table">
    @For Each role In ViewBag.rolenames
        @<tr>
            <td>@role</td>
        </tr>
    Next
</table>
<p>
    @Html.ActionLink("Edit", "Edit", New With { .id = Model.Id }) |
    @Html.ActionLink("Back to List", "Index")
</p>



