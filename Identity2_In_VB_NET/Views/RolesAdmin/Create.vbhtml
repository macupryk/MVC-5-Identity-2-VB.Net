@ModelType Identity2_In_VB_NET.Models.RoleViewModel
@Code
    ViewData("Title") = "Create"
End Code



<h2>Create New Role.</h2>
@Using Html.BeginForm()

    @Html.AntiForgeryToken()

    @<div class="form-horizontal">
        <h4>Role.</h4>
        <hr />
        @Html.ValidationSummary(True)

        <div class="form-group">
             @Html.LabelFor(Function(model) model.name, New With {.class = "control-label col-md-2"})
            <div class="col-md-10">
                @Html.TextBoxFor(Function(model) model.name, New With {.class = "form-control"})
                @Html.ValidationMessageFor(Function(model) model.name)
            </div>
        </div>
   
        <div class="form-group">
            <div class="col-md-offset-2 col-md-10">
                <input type="submit" value="Create" class="btn btn-default" />
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
       
       
