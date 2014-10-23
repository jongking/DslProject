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

//resize iframe cell
function dyniframesize(down) {
    var pTar = null;
    if (document.getElementById) {
        pTar = document.getElementById(down);
    } else {
        eval('pTar = ' + down + ';');
    }
    if (pTar && !window.opera) {
        //begin resizing iframe 
        pTar.style.display = "block";
        if (pTar.contentDocument && pTar.contentDocument.body.offsetHeight) {
            //ns6 syntax 
            pTar.height = pTar.contentDocument.body.offsetHeight + 20;
            pTar.width = pTar.contentDocument.body.scrollWidth + 20;
        } else if (pTar.Document && pTar.Document.body.scrollHeight) {
            //ie5+ syntax 
            pTar.height = pTar.Document.body.scrollHeight;
            pTar.width = pTar.Document.body.scrollWidth;
        }
    }
}