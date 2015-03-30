@Modeltype  Identity2_In_VB_NET.ApplicationRole
@Code
    ViewBag.Title = "Delete"
End Code

<h2>Role Delete.</h2>
<h3>Are you sure you want to delete this Role? </h3>
<p>Deleting this Role will remove all users from this role. It will not delete the users.</p>
<div>
    <h4>Delete Role.</h4>
    <hr />
    <dl class="dl-horizontal">
        <dt>
            @Html.DisplayNameFor(Function(Model) Model.Name)
        </dt>
        <dd>
            @Html.DisplayFor(Function(Model) Model.Name)
        </dd>
        
    </dl>
    @Using Html.BeginForm()
    
        @Html.AntiForgeryToken()

        @<div class="form-actions no-color">
            <input type="submit" value="Delete" class="btn btn-default" /> |
        @Html.ActionLink("Back to List", "Index")
        </div>
    End Using
</div>
