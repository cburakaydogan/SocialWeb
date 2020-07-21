$(document).ready(function () {
    var pageIndex = 1;
    if(typeof userName === 'undefined'){
        userName=null;
    }
    loadTweetList(pageIndex, userName);

    $(window).scroll(function () {
        if ($(window).scrollTop() + $(window).height() > $(document).height() - 50) {
            pageIndex = pageIndex + 1;
            loadTweetList(pageIndex, userName);
        }
    });
});

function loadTweetList(pageIndex, userName) {
  
    console.log(userName);
    $.ajax({
        url: "/Tweet/GetTweets",
        type: "POST",
        beforeSend: function () {
            $("#loader").show();
        },
        complete: function () {
            $("#loader").hide();
        },
        async: true,
        data: { pageIndex: pageIndex, userName: userName },
        success: function (result) {
            var html = '';
            console.log(result.length);
            if (result.length != 0) {
                $.each(result, function (key, item) {
                    html += '<li><i class="activity__list__icon fa fa-question-circle-o"></i><div class="activity__list__header"><img src="' + item.UserImage + '" alt="" /><a href="/profile/' + item.UserName + '">' + item.Name + '</a> @' + item.UserName + '</div><div class="activity__list__body entry-content"><p>' + item.Text + '</p></div><div class="activity__list__footer">';
                    if (item.isLiked) {
                        html += '<a onclick="Unlike(' + item.Id + ')" id="' + item.Id + '"> <i class="fa fa-heart"></i><span id="likecount_' + item.Id + '">' + item.LikesCount + '</span></a>';
                    }
                    else {
                        html += '<a onclick="Like(' + item.Id + ')" id="' + item.Id + '"> <i class="fa fa-heart-o"></i><span id="likecount_' + item.Id + '">' + item.LikesCount + '</span></a>';
                    }
                    html += '<a href="/tweet/' + item.Id + '"> <i class="fa fa-comments"></i>' + item.MentionsCount + '</><a href="#"> <i class="fa fa-share"></i>' + item.SharesCount + '</a><span><a href="/tweet/' + item.Id + '"> <i class="fa fa-clock"></i>' + formatTime(Date.parse(item.CreateDate)) + '</a></span></div></li>';
                });
                if (pageIndex == 1) {
                    console.log(pageIndex);
                    $('#TweetsList').html(html);
                }
                else {
                    console.log(pageIndex);
                    $('#TweetsList').append(html);
                }
            }
            else {
                $(window).unbind('scroll');
            }

        },
        error: function (errormessage) {
            alert(errormessage.responseText);
        }
    });
}