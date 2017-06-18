$(function () {

    var formSearch = $('input#search'),
        query = $('#qSearch').text(),
        search_content = $('#right_content #search_result'),
        live_search = $('#top_menu #live_search');

    color_searched_text(query, search_content);

    if ($('#qSearch').length > 0) formSearch.val(query).focus();



    formSearch.on('textchange', function () {
        var query = formSearch.val();

        if (typeof sTimeout != "undefined") clearTimeout(sTimeout);
        if (typeof hideTimeout != "undefined") clearTimeout(hideTimeout);
        live_search.html('<img src="/Content/images/ajax-loader.gif">');

        sTimeout = setTimeout(function () {
            $.ajax({
                type: "POST",
                url: "/Search/SearchJson",
                data: ({ 'SearchString': query }),
                success: function (json) {
                    var html = '';

                    for (i = 0; i < Math.min(10, json.length) ; i++) {
                        item = json[i];
                        html += '<p><a href="/Article/ViewArticle?articleId=' + item.Id + '">' + item.Title + '</a></p>';
                    }

                    live_search.html(html);

                    if (!empty(query)) color_searched_text(query, live_search);
                    live_search.fadeIn(200);

                    if (json.length > 0) {

                    } else {
                        hideTimeout = setTimeout(function () {
                            live_search.fadeOut(150);
                        }, 1000);

                        live_search.html(json.message);
                    }
                }
            });
        }, 350);

    });


    $('body').click(function () {
        live_search.fadeOut(150);
    });



    function color_searched_text(query, replaceable_text) {
        var reg_q = new RegExp(query, 'ig'),
            _this = replaceable_text,
            text = _this.text(),
            result = text.search(reg_q);

        if (query != '' && result != -1) {
            var matches = new Object();

            while ((result = reg_q.exec(text)) != null) {
                matches[result[0]] = '<span id="selection">' + result[0] + '</span>';
            }

            var result = _this.html().replace(
            new RegExp(Object.keys(matches).join('|'), 'g'),
            function (match) {
                return matches[match];
            });

            _this.html(result);
        }
    }


    function empty(mixed_var) {
        return (typeof mixed_var == "undefined" || mixed_var === "" || mixed_var === 0 || mixed_var === "0" || mixed_var === null || mixed_var === false || (typeof mixed_var == "string" && mixed_var.replace(/^\s+|\s+$/g, '') === '') || (is_array(mixed_var) && mixed_var.length === 0));
    }

    function is_array(mixed_var) {
        return (mixed_var instanceof Array);
    }
});


function input_clear_comment() {
    $("#article form[action*=AddComment] textarea").val('');
}


/*!
 * jQuery TextChange Plugin
 * http://www.zurb.com/playground/jquery-text-change-custom-event
 *
 * Copyright 2010, ZURB
 * Released under the MIT License
 */
(function (a) {
    a.event.special.textchange = {
        setup: function () { a(this).data("lastValue", this.contentEditable === "true" ? a(this).html() : a(this).val()); a(this).bind("keyup.textchange", a.event.special.textchange.handler); a(this).bind("cut.textchange paste.textchange input.textchange", a.event.special.textchange.delayedHandler) }, teardown: function () { a(this).unbind(".textchange") }, handler: function () { a.event.special.textchange.triggerIfChanged(a(this)) }, delayedHandler: function () {
            var c = a(this); setTimeout(function () { a.event.special.textchange.triggerIfChanged(c) },
            25)
        }, triggerIfChanged: function (a) { var b = a[0].contentEditable === "true" ? a.html() : a.val(); b !== a.data("lastValue") && (a.trigger("textchange", [a.data("lastValue")]), a.data("lastValue", b)) }
    }; a.event.special.hastext = { setup: function () { a(this).bind("textchange", a.event.special.hastext.handler) }, teardown: function () { a(this).unbind("textchange", a.event.special.hastext.handler) }, handler: function (c, b) { b === "" && b !== a(this).val() && a(this).trigger("hastext") } }; a.event.special.notext = {
        setup: function () {
            a(this).bind("textchange",
            a.event.special.notext.handler)
        }, teardown: function () { a(this).unbind("textchange", a.event.special.notext.handler) }, handler: function (c, b) { a(this).val() === "" && a(this).val() !== b && a(this).trigger("notext") }
    }
})(jQuery);
