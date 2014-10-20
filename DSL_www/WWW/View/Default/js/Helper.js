function GOTO(path) {
    var param = path.substring(1, path.length).split('/');
    var menuDiv = $("#menuDiv");
    menuDiv.find(".active").each(function() {
        $(this).attr("class", "");
    });
    menuDiv.find("#" + param[0] + "-dropdown").attr("class", "active");
    menuDiv.find("#" + param[1] + "-" + param[0] + "-dropdown").attr("class", "active");
    $("#MainContain").attr("src", path);
}