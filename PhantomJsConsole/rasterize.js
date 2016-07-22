"use strict";
var system = require('system');

if (system.args.length < 2) {
    phantom.exit(1);
} else {
    var url = system.args[1];
    var size = system.args[2].split('*');
    var page = require('webpage').create();
    page.viewportSize = { width: size[0], height: size[1] };
    page.open(url, function (status) {
        if (status !== "success") {
            phantom.exit();
        } else {
            waitFor(function () {
                return page.evaluate(function () {
                    var loading2 = $(".lockMask-loading2").css("display");
                    var img = typeof ($("img[isstopmove='false']").attr("src"));
                    return loading2 === "none" && img === "string";
                });
            }, function () {
                //return page.renderBase64();
                var pic = page.renderBase64();
                system.stdout.writeLine(pic);
                phantom.exit();
            });
        }
    });
}

function waitFor(testFx, onReady, system, timeOutMillis) {
    var maxtimeOutMillis = timeOutMillis ? timeOutMillis : 5000,
    start = new Date().getTime(),
    condition = false,
    interval = setInterval(function () {
        if ((new Date().getTime() - start < maxtimeOutMillis) && !condition) {
            condition = (typeof (testFx) === "string" ? eval(testFx) : testFx());
        } else {
            if (!condition) {
                clearInterval(interval);
                phantom.exit(1);
            } else {
                typeof (onReady) === "string" ? eval(onReady) : onReady();
                clearInterval(interval);
            }
        }
    }, 200);
}