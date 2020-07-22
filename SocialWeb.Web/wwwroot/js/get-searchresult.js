$(document).ready(function () {
    var pageIndex = 1;
    loadSearchResults(pageIndex, Searchkeyword);

    $(window).scroll(function () {
        if ($(window).scrollTop() + $(window).height() > $(document).height() - 50) {
            pageIndex = pageIndex + 1;
            loadSearchResults(pageIndex, Searchkeyword);
        }
    });
});

function loadSearchResults(pageIndex, Searchkeyword) {
    $.ajax({
        url: "/Search/SearchUser",
        type: "POST",
        beforeSend: function () {
            $("#loader").show();
        },
        complete: function () {
            $("#loader").hide();
        },
        async: true,
        data: { keyword: Searchkeyword, pageIndex: pageIndex },
        success: function (result) {
            var html = '';
            console.log(result);
            if (result.length != 0) {
                $.each(result, function (key, item) {
                    html += '<li><i class="activity__list__icon fa fa-question-circle-o"></i><div class="activity__list__header"><img src="' + item.ImagePath + '" alt="" /><a href="/profile/' + item.UserName + '">' + item.Name + '</a> @@'+item.UserName+'</div></li>';
                });
                if (pageIndex == 1) {
                    $('#SearchResult').html(html);
                }
                else {
                    $('#SearchResult').append(html);
                }
            }
            else {
                $('#SearchResult').html('<li><p class="text-center">There were no results found</p></li>');
                $(window).unbind('scroll');
            }

        },
        error: function (errormessage) {
            $('#SearchResult').html('<li><p class="text-center">There were no results found</p></li>');
                $(window).unbind('scroll');
        }
    });
}