@Modeltype IEnumerable(Of Microsoft.AspNet.Identity.EntityFramework.IdentityRole)
@Code
    ViewData("Title") = "Index"
End Code

<h2>Roles Index</h2>

<p>
@Html.ActionLink("Create New", "Create")
</p>
<table class="table">
    <tr>
        <th>
            @Html.DisplayNameFor(Function(Model) Model.Name)
        </th>
       
        <th>
        </th>
    </tr>


    @For Each item In Model
    @<tr>
        <td>
            @Html.DisplayFor(Function(modelItem) item.Name)
        </td>

        <td>
            @Html.ActionLink("Edit", "Edit", New With {.id = item.Id}) |
            @Html.ActionLink("Details", "Details", New With {.id = item.Id}) |
            @Html.ActionLink("Delete", "Delete", New With {.id = item.Id})
        </td>
    </tr>

    Next
</table>
