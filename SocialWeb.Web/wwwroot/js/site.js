function formatTime(timeCreated) {

    var diff = Math.floor((Date.now() - timeCreated) / 1000);
    var interval = Math.floor(diff / 31536000);

    if (interval >= 1) {
        return interval + "y";
    }
    interval = Math.floor(diff / 2592000);
    if (interval >= 1) {
        return interval + "m";
    }
    interval = Math.floor(diff / 604800);
    if (interval >= 1) {
        return interval + "w";
    }
    interval = Math.floor(diff / 86400);
    if (interval >= 1) {
        return interval + "d";
    }
    interval = Math.floor(diff / 3600);
    if (interval >= 1) {
        return interval + "h";
    }
    interval = Math.floor(diff / 60);
    if (interval >= 1) {
        return interval + " m";
    }
    return "<1m";
}

function Like(id) {
    let $likecount = $("#likecount_" + id);
    let likecount = Number($likecount.val() + $likecount.next().text()) || 0;

    var model = {
        TweetId: id,
        AppUserId: 0
    };
    $.ajax({
        data: JSON.stringify(model),
        type: "POST",
        url: "/Like/Like/",
        contentType: "application/json",
        dataType: "JSON",
        success: function (result) {
            if (result == "Success") {
                likecount = likecount + 1;
                let html = '<a onclick="Unlike(' + id + ')"id="' + id + '"><i class="fa fa-heart"></i><span id="likecount' + id + '">' + likecount + '</span></a>';

                $("#" + id).replaceWith(html);
            }
        }
    });
}
function Unlike(id) {
    let $likecount = $("#likecount_" + id);
    let likecount = Number($likecount.val() + $likecount.next().text()) || 0;
    var model = {
        TweetId: id,
        AppUserId: 0
    };
    $.ajax({
        data: JSON.stringify(model),
        type: "POST",
        url: "/Like/Unlike/",
        contentType: "application/json",
        dataType: "JSON",
        success: function (result) {
            if (result == "Success") {

                if (likecount != 0)
                    likecount = likecount - 1;

                let html = '<a onclick="Like(' + id + ')"id="' + id + '"><i class="fa fa-heart-o"></i><span id="likecount' + id + '">' + likecount + '</span></a>';

                $("#" + id).replaceWith(html);
            }

        }
    });
}

$(document).ready(function () {

    $('#btnSendTweet').click(function (e) {
        var formData = new FormData();

        formData.append("AppUserId", JSON.stringify(parseInt($("#AppUserId").val())));
        formData.append("Text", $("#Text").val());
        formData.append("Image", document.getElementById("Image").files[0]);

        $.ajax({
            data: formData,
            processData: false,
            contentType: false,
            type: "POST",
            url: "/Tweet/AddTweet/",
            success: function (result) {
                if (result == "Success") {
                    $("#tweetValidation").addClass("alert alert-success").text("Sent Successfully!");
                    $("#tweetValidation").alert();
                    $("#tweetValidation").fadeTo(3000, 3000).slideUp(800, function () {
                    });
                }
                else {
                    $("#tweetValidation").addClass("alert alert-danger").text("Error Occured!");
                    $("#tweetValidation").alert();
                    $("#tweetValidation").fadeTo(3000, 3000).slideUp(800, function () {
                    });
                }
            }
        });
    });
});

function Follow(isExist) {
    var model = {
        FollowerId: parseInt($("#FollowerId").val()),
        FollowingId: parseInt($("#FollowingId").val()),
        isExist: isExist
    };
    console.log(FollowersCount,FollowingsCount);
    $.ajax({
        data: { FollowerId: model.FollowerId, FollowingId: model.FollowingId, isExist: model.isExist },
        type: "POST",
        url: "/Follow/Follow/",
        dataType: "JSON",
        success: function (result) {
            if (result == "Success") {
                if (!isExist) {
                    $("#Follow").replaceWith('<button onclick="Follow(true)" id="UnFollow" class="btn btn-rounded btn-info"><i class="fa fa-minus"></i> Unfollow</button>');
                    FollowersCount = FollowersCount + 1;
                    $("#FollowersCount").replaceWith('<li id="FollowersCount"><strong>'+ FollowersCount+'</strong>Followers</li>');
                }
                else {
                    $("#UnFollow").replaceWith('<button onclick="Follow(false)" id="Follow" type="submit" class="btn btn-rounded btn-info"><i class="fa fa-plus"></i> Follow</button>');
                    FollowersCount = FollowersCount - 1;
                    $("#FollowersCount").replaceWith('<li id="FollowersCount"><strong>'+ FollowersCount+'</strong>Followers</li>');
                }
            }
        }
    });
}

/* left navbar */
$(document).ready(function () {
    var url = window.location.href;
    $('a.list-group-item').each(function () {
        if (this.href == url) {
            $(this).addClass('active');
            return false;
        }
    });
});

$(document).ready(function () {

    $('#btnSendMention').click(function (e) {
        var model = {
            Text: $("#Text").val(),
            TweetId: parseInt($("#Id").val())
        };

        $.ajax({
            data: model,
            type: "POST",
            dataType: "JSON",
            url: "/Mention/AddMention/",
            success: function (result) {
                if (result == "Success") {
                    $("#tweetValidation").addClass("alert alert-success").text("Sent Successfully!");
                    $("#tweetValidation").alert();
                    $("#tweetValidation").fadeTo(3000, 3000).slideUp(800, function () {
                    });
                }
                else {
                    $("#tweetValidation").addClass("alert alert-danger").text("Error Occured!");
                    $("#tweetValidation").alert();
                    $("#tweetValidation").fadeTo(3000, 3000).slideUp(800, function () {
                    });
                }
            }
        });
    });
});  