
$(function () {
    
    $("#fileFinder").on("keyup", function () {
        $this = $(this);
        $searchResults = $("#searchResults");
        $searchResults.show();

        if ($this.val().length == 0) {
            $searchResults.html('');
        }
        else
        {
            $.ajax(
                "/FileStorages/GetList?s=" + encodeURIComponent($this.val()),
                {
                    accepts: "html",
                    success: function (data) {
                        $searchResults.html(data);
                        $(".search-result-item").on("click", function () {
                            $searchResults.hide();
                            var itemId = $(this).attr("data-file-id");
                            var itemText = $(this).html();
                            
                            var optionItem = "<option value=" + itemId + ">" + itemText + "</option>";
                            
                            $("#selectedFiles").append(optionItem);

                        });
                    },
                    statusCode: {
                        404: function ()
                        {
                            alert("search page not found");
                        }
                    }
                }
            )
        }
    });
})

