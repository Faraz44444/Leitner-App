$(document).ready(function () {
    moment.updateLocale('en', {
        week: { dow: 1 } // Monday is the first day of the week
    });

    // SET FOCUS TO INPUT IF CUSTOM-CONTROL IS CLICKED (CHECK-BOXES INSIDE CUSTOM-INPUT-GROUP)
    $("div").on("click", ".custom-control", function (e) {
        if (event.target == $(this).find("input")) return;
        if (event.target == this) {
            e.stopPropagation();
            $(this).find("input").click();
        }
    });

    // SET CLASS DISABLED TO CUSTOM-INPUT-GROUP CONTAINER IF INPUT HAS ATTR DISABLED
    $(".custom-control > input[disabled]").parent().addClass("disabled");

    // ADD 'REQUIRED' TO CUSTOM INPUT GROUP LABEL IF INPUT/SELECT IS REQUIRED
    $(".custom-input-group input[required], .custom-input-group select[required]").parent().find("label").append("<span class='ml-1 small text-danger'>Required</span>");

    // SETS PAGE HEIGHT TO >= WINDOW HEIGHT
    function pageHeight() {
        $.leftNav = $("#mainNavigation");
        $.root = $("body");
        $.navAndContentContainer = $("#NavAndContentContainer")

        var mainHeight = $("#main").height();
        $.offsetTop = $("#topNavigation").innerHeight();
        var mainMinHeight = $(window).height() - $.offsetTop;

        if (mainHeight > mainMinHeight) {
            $.root.css('min-height', mainHeight + $.offsetTop + 'px');
            $.navAndContentContainer.css("min-height", mainHeight + "px");
            $.leftNav.css("min-height", mainHeight + "px");
        } else {
            $.root.css('min-height', mainMinHeight + $.offsetTop + 'px');
            $.navAndContentContainer.css("min-height", mainMinHeight + "px");
            $.leftNav.css("min-height", mainMinHeight + "px");
        }
    }

    // SETS TABLAE CONTAINER MAX HEIGHT --> REACH BOTTOM OF SCREEN
    function setTableContainerMaxHeight() {
        let tables = $("div.table-responsive:not(.ignore-height-script)");
        if (!tables || tables.length == 0)
            return;

        let maxHeight = 0;
        let minHeight = 0;
        let offsetTop = 0;
        let height = 0;
        let offsetBottom = 0;
        let parents = [];
        for (i = 0; i < tables.length; i++) {
            offsetTop = $(tables[i]).offset().top;
            minHeight = parseFloat($(tables[i]).data("minHeight"));
            if (isNaN(minHeight))
                minHeight = 0;
            maxHeight = parseFloat($(tables[i]).data("maxHeight"));
            if (isNaN(maxHeight))
                maxHeight = 0;
            height = $(window).height() - offsetTop;
            offsetBottom = 0;
            parents = $(tables[i]).parents();
            for (x = 0; x < parents.length; x++) {
                offsetBottom += parseFloat($(parents[x]).css("padding-bottom"));
                offsetBottom += parseFloat($(parents[x]).css("border-bottom"));
            }
            height = height - offsetBottom;
            if (height < 300)
                height = minHeight;
            if (maxHeight > 0 && maxHeight < height)
                height = maxHeight;
            tables[i].cssMaxheight = height;
        }

        for (i = 0; i < tables.length; i++) {
            if (tables[i].cssMaxheight > 0) {
                $(tables[i]).css("max-height", tables[i].cssMaxheight + "px");
                $(tables[i]).css("padding-right", ".1rem");
            } else {
                $(tables[i]).css("max-height", "");
                $(tables[i]).css("padding-right", "");
            }

        }
    }

    // HANDLES CLICK EVENT ON MENU ITEMS WITH CHILD ITEMS
    $("#mainNavigation a[href='#']").on("click", function (e) {
        e.preventDefault();
        var el = $(this).parent();
        if (el.hasClass("disabled"))
            return;

        var opened = el.hasClass("opened");

        if ($("#NavAndContentContainer").hasClass("minified"))
            $("#mainNavigation li").removeClass("opened");

        if (!opened) {
            el.addClass("opened");
        } else {
            el.removeClass("opened");
        }
    });

    // HANDLES TOGGLING MOBILE MENU
    $("#btnToggleMobileMenu").on("click", function (e) {
        e.preventDefault();
        var mainNav = $("#topMainNavigation");
        if (mainNav.hasClass("toggled")) {
            mainNav.removeClass("toggled");
        } else {
            mainNav.addClass("toggled");
        }
    });


    // HANDLES MENU MINIFCATION TOGGLING
    let navWidthEvent;
    $("#NavWidthButton").on("click", function () {
        let leftNavigationMinified = false;
        if ($("#NavAndContentContainer").hasClass("minified")) {
            $("#NavAndContentContainer").removeClass("minified");
            sessionStorage.setItem("minified", false);
        } else {
            $("#NavAndContentContainer").addClass("minified");
            sessionStorage.setItem("minified", true);
            $("#mainNavigation li").removeClass("opened");
            leftNavigationMinified = true;
        }
        navWidthEvent = document.createEvent("Event");
        navWidthEvent.initEvent("navWidth", false, true);
        document.dispatchEvent(navWidthEvent);

        apiService.PostRequest("account/updateLeftNavigationMinified", { LeftNavigationMinified: leftNavigationMinified }).then(function () {
        }, function (error) {
            feedback.DisplayError(error);
        });
    });

    //if (sessionStorage.getItem("minified") === "true") {
    //    $("#NavAndContentContainer").addClass("minified");
    //    $("#mainNavigation li").removeClass("opened");
    //}
    if ($("#NavAndContentContainer").hasClass("minified")) {
        $("#mainNavigation li").removeClass("opened");
    }

    $("#NavAndContentContainer").show();

    // HANDLES CHANGING SITE FROM TOP NAVIGATION
    $(".availableSiteItem").on("click", function () {
        let el = $(this);
        let siteId = el.data("siteId");
        let supplierId = el.data("supplierId");
        let warehouseId = el.data("warehouseId");
        let clientId = el.data("clientId");

        if (siteId > 0) {
            apiService.PostRequest("account/updateCurrentSite", { NewCurrentSiteId: siteId }).then(function () {
                window.location.replace("/dashboard");
            }, function (error) {
                feedback.DisplayError(error);
            });
        } else if (supplierId > 0) {
            apiService.PostRequest("account/updateCurrentSupplier", { NewCurrentSupplierId: supplierId }).then(function () {
                window.location.replace("/dashboard");
            }, function (error) {
                feedback.DisplayError(error);
            });

        } else if (warehouseId > 0) {
            apiService.PostRequest("account/updateCurrentWarehouse", { NewCurrentWarehouseId: warehouseId }).then(function () {
                window.location.replace("/dashboard");
            }, function (error) {
                feedback.DisplayError(error);
            });

        } else if (warehouseId > 0) {
            apiService.PostRequest("account/updateCurrentClient", { NewClientId: clientId }).then(function () {
                window.location.replace("/dashboard");
            }, function (error) {
                feedback.DisplayError(error);
            });
        }


    });

    $(".availableClientItem").on("click", function () {
        let el = $(this);    
        let supplierId = el.data("supplierId");      
        let clientId = el.data("clientId");

        if (clientId > 0) {
            apiService.PostRequest("account/updateCurrentClient", { NewCurrentClientId: clientId }).then(function () {
                window.location.replace("/dashboard");
            }, function (error) {
                feedback.DisplayError(error);
            });
 
        }
        //else if (supplierId > 0) {
        //    apiService.PostRequest("account/updateCurrentSupplier", { NewCurrentSupplierId: supplierId }).then(function () {
        //        window.location.replace("/dashboard");
        //    }, function (error) {
        //        feedback.DisplayError(error);
        //    });
        //}


    });

    $("#switchUserType").on("click", function () {
        apiService.PostRequest("account/updateUserType").then(function () {
            window.location.replace("/dashboard");
        }, function (error) {
            feedback.DisplayError(error);
        });
    });


    // TRIGGER CALC MENU HEIGHT & OBSERVE DOM
    $(window).resize(function () {
        pageHeight();
        setTableContainerMaxHeight();
    });
    pageHeight();
    document.addEventListener("calcTableMaxHeight", function () {
        pageHeight();
        setTableContainerMaxHeight();
    });

    // Timeout is set because we are using vue component on sections. If timeout is removed this is done before elements are rendered
    //let tableContainerHeightTimeout = setTimeout(function () {
    //    setTableContainerMaxHeight();
    //}, 800);
    observeDOM(document.querySelector('#main'), pageHeight);
});

var observeDOM = (function () {
    var MutationObserver = window.MutationObserver || window.WebKitMutationObserver;

    return function (obj, callback) {
        if (!obj || !obj.nodeType === 1) return; // validation

        if (MutationObserver) {
            // define a new observer
            var obs = new MutationObserver(function (mutations, observer) {
                callback(mutations);
            });
            // have the observer observe foo for changes in children
            obs.observe(obj, { childList: true, subtree: true, attributeFilter: ['style', 'class'] });
        }
        else if (window.addEventListener) {
            obj.addEventListener('DOMNodeInserted', callback, false);
            obj.addEventListener('DOMNodeRemoved', callback, false);
        }
    };
})();