$(document).ready(function () {
    $.ajax({
        url: '/Yazi/YaziOku',
        data: { YaziId: @Model.YaziId},
        type: 'POST',
    });
});