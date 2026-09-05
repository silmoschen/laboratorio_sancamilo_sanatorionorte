(function ($) {
    if (!window.bootstrap || !$) {
        return;
    }

    $.fn.modal = function (action) {
        return this.each(function () {
            var instance = bootstrap.Modal.getOrCreateInstance(this);
            if (action === "show") {
                instance.show();
            } else if (action === "hide") {
                instance.hide();
            } else if (action === "toggle") {
                instance.toggle();
            }
        });
    };
})(jQuery);
