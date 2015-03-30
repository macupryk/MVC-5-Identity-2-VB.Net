@ModelType Identity2_In_VB_NET.RegisterViewModel
@Code
    ViewData("Title") = "Create"
End Code

<h2>Create New User</h2>

@Using (Html.BeginForm("Create", "UsersAdmin", FormMethod.Post, New With {.class = "form-horizontal", .role = "form"}))
    @Html.AntiForgeryToken()
    
    @<div class="form-horizontal">
         <h4>Create a new account.</h4>
         <hr />
         @Html.ValidationSummary("", New With {.class = "text-error"})
         <div class="form-group">
             @Html.LabelFor(Function(model) model.Email, New With {.class = "col-md-2 control-label"})
             <div class="col-md-10">
                 @Html.TextBoxFor(Function(model) model.Email, New With {.class = "form-control"})
             </div>
         </div>
         <div class="form-group">
             @Html.LabelFor(Function(model) model.Password, New With {.class = "col-md-2 control-label"})
             <div class="col-md-10">
                 @Html.PasswordFor(Function(model) model.Password, New With {.class = "form-control"})
             </div>
         </div>
         <div class="form-group">
             @Html.LabelFor(Function(model) model.ConfirmPassword, New With {.class = "col-md-2 control-label"})
             <div class="col-md-10">
                 @Html.PasswordFor(Function(model) model.ConfirmPassword, New With {.class = "form-control"})
             </div>
         </div>
         <div class="form-group">
             <label class="col-md-2 control-label">
                 Select User Role
             </label>
             <div class="col-md-10">
               
                 @For Each item As SelectListItem In ViewBag.roleid
                    @<input type="checkbox" name="SelectedRoles" value="@item.Value" class="checkbox-inline" />
                    @Html.Label(item.Value, New With {.class = "control-label"})
                 Next
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

@Section Scripts 
    @Scripts.Render("~/bundles/jqueryval")
End Section
