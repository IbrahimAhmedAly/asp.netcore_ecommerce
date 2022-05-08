let Items = [];

let ClsItems = {
    LoadItems: function () {
        Helper.AjaxCallGet("/api/ItemsApi", {}, "json", function (data) {
            Items = data;
            //console.log(data);
            let htmlData = "";
            for (let i = 0; i < data.length; i++) {
                htmlData = htmlData + ClsItems.Template1(data[i]);
            }
            $("#DivTemplate1").html(htmlData);
        },
            function () {

            });
    },
    Template1: function (item) {
        let itemHtml = "<div class='product product__style--3 col-lg-4 col-md-4 col-sm-6 col-12'>";
        itemHtml = itemHtml + "<div class='product__thumb'>";
        itemHtml = itemHtml + "<div class='product__thumb'>";
        itemHtml = itemHtml + " <a class='first__img' href='/Items/Details/" + item.itemId + "'><img src='/Uploads/1.jpg' alt='product image'></a>";
        itemHtml = itemHtml + "<a class='second__img animation1' href='/Items/Details/" + item.itemId + "'><img src='/Uploads/1.jpg' alt='product image'></a>";
        itemHtml = itemHtml + " <div class='hot__box'>";
        itemHtml = itemHtml + " <span class='hot - label'>" + item.categoryName + "</span> </div></div>";
        itemHtml = itemHtml + " <div class='product__content content--center'>";
        itemHtml = itemHtml + " <h4><a href='/Items/Details/" + item.itemId + "'>" + item.itemName + "</a></h4>";
        itemHtml = itemHtml + " <ul class='prize d-flex'> <li>" + item.salesPrice + "</li><li class='old_prize'>" + item.salesPrice + "</li> </ul>";
        itemHtml = itemHtml + " <div class='action'><div class='actions_inner'><ul class='add_to_links'>";
        itemHtml = itemHtml + "  <li><a class='cart' href='/Items/Details/" + item.itemId + "'><i class='bi bi-shopping-bag4'></i></a></li>";
        itemHtml = itemHtml + " <li><a class='wishlist' href='wishlist.html'><i class='bi bi-shopping - cart-full'></i></a></li>";
        itemHtml = itemHtml + " <li><a class='compare' href='#'><i class='bi bi - heart-beat'></i></a></li>";
        itemHtml = itemHtml + " <li><a data-toggle='modal' title='Quick View' class='quickview modal-view detail-link' href='#productmodal'><i class='bi bi-search'></i></a></li></ul></div></div>";
        itemHtml = itemHtml + " <div class='product__hover--content'>";
        itemHtml = itemHtml + " <ul class='rating d-flex'>";
        itemHtml = itemHtml + " <li class='on'><i class='fa fa-star-o'></i></li>";
        itemHtml = itemHtml + " <li class='on'><i class='fa fa-star-o'></i></li>";
        itemHtml = itemHtml + " <li class='on'><i class='fa fa-star-o'></i></li>";
        itemHtml = itemHtml + " <li><i class='fa fa-star-o'></i></li>";
        itemHtml = itemHtml + " <li><i class='fa fa-star-o'></i></li> </ul> </div></div></div></div>";

        return itemHtml;
    }
    ,
    FilterItems: function (catId) {
        let newItems = $.grep(Items, function (n, i) { // just use arr
            return n.categoryId === catId;
        });
        console.log(newItems);
        let htmlData = "";
        for (let i = 0; i < newItems.length; i++) {
            htmlData = htmlData + ClsItems.Template1(newItems[i]);
        }
        $("#DivTemplate1").html(htmlData);
    }
}

ClsItems.LoadItems();
