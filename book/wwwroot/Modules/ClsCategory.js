﻿let ClsCategory =
{
    LoadCategories: function () {
        Helper.AjaxCallGet("/api/CategoryApi", {}, "json", function (data) {
            let htmlData = "";
            for (let i = 0; i < data.length; i++) {
                htmlData = htmlData + ClsCategory.Template1(data[i]);
            }
            $("#Categories").html(htmlData);
        },
            function () {

            });
    },
    Template1: function (cat) {
        let cattegory = " <li><a onclick='ClsItems.FilterItems(" + cat.categoryId+")'>" + cat.categoryName + " <span>(3)</span></a></li>";
        return cattegory;
    }
}
ClsCategory.LoadCategories();
