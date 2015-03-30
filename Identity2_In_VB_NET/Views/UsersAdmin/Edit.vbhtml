@ModelType Identity2_In_VB_NET.models.EditUserViewModel
@Code
    ViewData("Title") = "Edit"
End Code

<h2>Edit User</h2>

@Using (Html.BeginForm())
    @Html.AntiForgeryToken()
    
    @<div class="form-horizontal">
         <h4>Edit User Form.</h4>
        <hr />
        @Html.ValidationSummary(True, "", New With { .class = "text-danger" })
        @Html.HiddenFor(Function(model) model.Id)

        <div class="form-group">
            @Html.LabelFor(Function(model) model.Email, htmlAttributes:= New With { .class = "control-label col-md-2" })
            <div class="col-md-10">
                @Html.EditorFor(Function(model) model.Email, New With { .htmlAttributes = New With { .class = "form-control" } })
                @Html.ValidationMessageFor(Function(model) model.Email, "", New With { .class = "text-danger" })
            </div>
        </div>

         <div class="form-group">
             @Html.Label("Roles", New With {.class = "control-label col-md-2"})
             <span class=" col-md-10">
                 @For Each item In Model.RolesList
                
                 @<input type="checkbox" name="SelectedRole" value="@item.Value" checked="@item.Selected" class="checkbox-inline" />
                 @Html.Label(item.Value, New With {.class = "control-label"})
                 next
             </span>
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

@Section Scripts 
    @Scripts.Render("~/bundles/jqueryval")
End Section

