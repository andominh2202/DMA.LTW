$(function () {
    var content = $("#content");
    var request;

    function updateLinks() {
        var results = $("#learner-results");
        var mid = results.attr("data-mid") || "";
        var keyword = results.attr("data-keyword") || "";
        var useAjax = $("#use-ajax").prop("checked");
        $("#selected-major").val(mid);
        $("#major-filter a").each(function () {
            var link = $(this);
            var url = new URL(this.href);
            url.searchParams.set("keyword", keyword);
            url.searchParams.set("ajax", useAjax);
            this.href = url.toString();
            link.toggleClass("active", link.attr("data-mid") === mid);
        });
        $("#content .page-link").each(function () {
            var url = new URL(this.href);
            url.searchParams.set("ajax", useAjax);
            this.href = url.toString();
        });
    }

    function loadLearners(url) {
        if (request) request.abort();
        $("#filter-error").addClass("d-none");
        content.attr("aria-busy", "true");
        request = $.ajax({
            url: content.attr("data-filter-url"),
            data: {
                mid: url.searchParams.get("mid") || "",
                keyword: url.searchParams.get("keyword") || "",
                pageIndex: url.searchParams.get("pageIndex") || 1
            }
        }).done(function (response) {
            content.html(response);
            $("#keyword").val($("#learner-results").attr("data-keyword"));
            url.searchParams.set("pageIndex", $("#learner-results").attr("data-page"));
            url.searchParams.set("ajax", "true");
            window.history.replaceState(null, "", url.toString());
            updateLinks();
        }).fail(function (_, status) {
            if (status !== "abort") $("#filter-error").removeClass("d-none");
        }).always(function () {
            content.attr("aria-busy", "false");
        });
    }

    $("#major-filter").on("click", "a", function (event) {
        if (!$("#use-ajax").prop("checked") || event.ctrlKey || event.metaKey || event.shiftKey || event.altKey) return;
        event.preventDefault();
        loadLearners(new URL(this.href));
    });

    content.on("click", ".page-link", function (event) {
        if (!$("#use-ajax").prop("checked") || event.ctrlKey || event.metaKey || event.shiftKey || event.altKey) return;
        event.preventDefault();
        loadLearners(new URL(this.href));
    });

    $("#learner-search").on("submit", function (event) {
        if (!$("#use-ajax").prop("checked")) return;
        event.preventDefault();
        var url = new URL(this.action);
        url.searchParams.set("mid", $("#selected-major").val());
        url.searchParams.set("keyword", $("#keyword").val());
        loadLearners(url);
    });

    $("#use-ajax").on("change", updateLinks);
    updateLinks();
});
