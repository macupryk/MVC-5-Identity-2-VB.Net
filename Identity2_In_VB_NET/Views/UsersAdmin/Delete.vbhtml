@ModelType Identity2_In_VB_NET.ApplicationUser
@Code
    ViewData("Title") = "Delete"
End Code

<h2>Delete User</h2>

<h3>Are you sure you want to delete this User.?</h3>
<div>
   
    <hr />
    <dl class="dl-horizontal">
        <dt>
            @Html.DisplayNameFor(Function(model) model.Email)
        </dt>

        <dd>
            @Html.DisplayFor(Function(model) model.Email)
        </dd>

    </dl>
    @Using (Html.BeginForm())
        @Html.AntiForgeryToken()

        @<div class="form-actions no-color">
            <input type="submit" value="Delete" class="btn btn-default" /> |
            @Html.ActionLink("Back to List", "Index")
        </div>
    End Using
</div>

