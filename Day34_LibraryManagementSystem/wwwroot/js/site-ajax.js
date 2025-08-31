(function(){
  const $doc = $(document);

  // Open modal with partial form
  $doc.on("click", "[data-ajax='true']", function () {
    const url = $(this).data("url");
    const title = $(this).data("title") || "";
    $.get(url, function (html) {
      $("#ajaxModalContent").html(html);
      const modal = new bootstrap.Modal(document.getElementById("ajaxModal"));
      modal.show();
    });
  });

  // Handle AJAX form submit (Create/Edit)
  $doc.on("submit", "#ajaxForm", function (e) {
    e.preventDefault();
    const $form = $(this);
    const url = $form.attr("action");
    $.ajax({
      url: url,
      type: "POST",
      data: $form.serialize(),
      success: function (res) {
        if (res.success) {
          // refresh list area if present
          // simple approach: reload current page list via GET (AJAX if Index supports it)
          const $list = $("#listContainer");
          if ($list.length) {
            $.get(window.location.pathname + window.location.search, function (html) {
              $list.html($(html));
            });
          } else {
            location.reload();
          }
          bootstrap.Modal.getInstance(document.getElementById("ajaxModal")).hide();
        } else if (res.html) {
          $("#ajaxModalContent").html(res.html); // return form with errors
        } else if (res.error) {
          $("#formError").text(res.error);
        }
      },
      error: function (xhr) {
        const msg = (xhr.responseJSON && xhr.responseJSON.error) ? xhr.responseJSON.error : "Unexpected error";
        $("#formError").text(msg);
      }
    });
  });

  // Delete with confirmation
  $doc.on("click", "[data-delete-url]", function(){
    const url = $(this).data("delete-url");
    if (!confirm("Delete this item?")) return;
    $.post(url, {}, function(res){
      if (res.success){
        const $list = $("#listContainer");
        if ($list.length) {
          $.get(window.location.pathname + window.location.search, function (html) {
            $list.html($(html));
          });
        } else {
          location.reload();
        }
      }
    }).fail(function(xhr){
      alert((xhr.responseJSON && xhr.responseJSON.error) || "Delete failed");
    });
  });
})();