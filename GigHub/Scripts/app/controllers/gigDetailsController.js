// I created a new controller called GigDetailsController.
//So, this is a new module and it has a dependency to another module called followingService,
//which is responsible for calling the API. 
var GigDetailsController = function (followingService) {
    var followButton;

    // In the inIt method we subscribe to the click event of buttons with the class js, toggle, follow.
    var init = function () {
        $(".js-toggle-follow").click(toggleFollowing);
    };

    //Here's our click handler, toggleFollowing
    var toggleFollowing = function (e) {
        //first we get a reference to the button that was clicked,
        followButton = $(e.target);
        //from there, we get the followeeId which is a data attribute on the button,
        var followeeId = followButton.attr("data-user-id");

        //and then we check if the button has the class btn-default. 
        if (followButton.hasClass("btn-default"))
            //We ask followingService to create a following object; 
            followingService.createFollowing(followeeId, done, fail);
        else
            //otherwise, we delete the following.
            followingService.deleteFollowing(followeeId, done, fail);
    };

    var done = function () {
        var text = (followButton.text() == "Follow") ? "Following" : "Follow";
        //In the done method we toggle the btn-info and the btn-default classes and change the text.
        followButton.toggleClass("btn-info").toggleClass("btn-default").text(text);
    };

    var fail = function () {
        // And in the fail method, for now we just display a simple alert.
        alert("Something failed!");
    };

    return {
        init: init
    }
}(FollowingService);