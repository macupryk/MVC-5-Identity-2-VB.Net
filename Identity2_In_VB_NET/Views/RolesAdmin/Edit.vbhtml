@ModelType Identity2_In_VB_NET.Models.RoleViewModel
@Code
    ViewData("Title") = "Edit"
End Code

<h2>Role Edit</h2>

@Using Html.BeginForm()

    @Html.AntiForgeryToken()

    @<div class="form-horizontal">
        <h4>Roles.</h4>
        <hr />
        @Html.ValidationSummary(True)
        @Html.HiddenFor(Function(Model) Model.id)

        <div class="form-group">
             @Html.LabelFor(Function(Model) Model.name, New With {.class = "control-label col-md-2"})
            <div class="col-md-10">
                 @Html.TextBoxFor(Function(Model) Model.name, New With {.class = "form-control"})
                 @Html.ValidationMessageFor(Function(Model) Model.name)
            </div>
        </div>
    

        <div class="form-group">
            <div class="col-md-offset-2 col-md-10">
                <input type="submit" value="Save" class="btn btn-default" />
            </div>
        </div>
    </div>
End Using

<div>
    @Html.ActionLink("Back to List", "Index")
</div>

    @section Scripts
        @Scripts.Render("~/bundles/jqueryval")
    End Section 
      
        
