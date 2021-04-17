// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
$("#search").bind("input propertychange", function () {
    var value = $(this).val();
    if (!value) {

        window.location.href = "./Home/Index";
    }
});