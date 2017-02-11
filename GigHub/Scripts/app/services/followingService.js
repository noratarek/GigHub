// So, this service is another module that has two methods: createFollowing and deleteFollowing.
//In each of these methods I used jQuery ajax to call our API end point.
var FollowingService = function () {
    var createFollowing = function (followeeId, done, fail) {
        $.post("/api/followings", { followeeId: followeeId })
        .done(done)
        .fail(fail);

    };

    var deleteFollowing = function (followeeId, done, fail) {
        $.ajax({
            url: "/api/followings" + followeeId,
            mehod: "DELETE"
        })
        .done(done)
        .fail(fail);
    };

    return {
        createFollowing: createFollowing,
        deleteFollowing: deleteFollowing
    }
}();