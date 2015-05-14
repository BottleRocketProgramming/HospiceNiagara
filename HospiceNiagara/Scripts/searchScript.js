
/*
    This is the "hacky", simplified way to do it, just paste in some HTML that resulted from the ajax call.
    If the ajax call returned a JSON result, you would have to build your HTML entities programmatically.

    I use this as a shortcut if my scripts are the only software consuming the ajax data. If I were serving the
    data to somebody else, or using it on multiple platforms, I would definitely serve it as JSON, since it 
    allows portability to all platforms, even if they don't use HTML (for example a native Android or iOS app)

    *************************************************************************************************************
     I swear this is much simpler than it looks. The comments bloated it up, if you remove them there are only 
     a dozen or so lines of code.
    *************************************************************************************************************

*/
$(function () {
    $("#fileFinder").on("keyup", function () {

        // Store the jquery objects in reusable variables since we call them more than once. 
        // (Note: the $ prefix on the variable name is a convention to remind us that the object contains a jquery object)
        $this = $(this);
        $searchResults = $("#searchResults");

        if ($this.val().length == 0) {
            // If the search field is empty, we'll return no results.
            $searchResults.html('');
        } else {
            // If we have a value in the search field, we'll query the controller for some data. 
            // (Read more about $.ajax at http://api.jquery.com/jquery.ajax/) 
            $.ajax(
                // First parameter is the URL we're sending the ajax call to
                "/FileStorage/GetList?s=" + encodeURIComponent($this.val()),
                // Second parameter is a javascript object that tells jquery how to execute the call and 
                // how to respond. There are many more customizable parameters you can use, but these are 
                // usually all you need.
                {
                    // We will be getting html back, jquery sometimes doesn't handle the data correctly if we don't do this.
                    accepts: "html",
                    // This is the "callback" function we want to be used if we get successful results.
                    success: function (data) {
                        $searchResults.html(data);
                        $(".search-result-item").on("click", function () {

                            // You could just as easily put the jquery below directly into the .append() call,
                            // but I broke them out into variables to make it easier to read.
                            var itemId = $(this).attr("data-file-id");
                            var itemText = $(this).html();

                            // Build the HTML for a new option item which is to be placed in the selected items box.
                            // You could improve on the UX in many ways, but this will work with what you currently have.
                            var optionItem = "<option value=" + itemId + ">" + itemText + "</option>";

                            // Add the <option> item to $("#selectedFiles")
                            $("#selectedFiles").append(optionItem);

                        });
                    },
                    // If something messes up, you'll probably get a 404 not found status back from the server, so this will
                    // give you feedback. The way its currently written, its really only useful to the developer (you). 
                    // I might use this as a signal fall back to the more clunky server side way of doing things.
                    statusCode: {
                        404: function () {
                            alert("search page not found");
                        }
                    }
                }
            )
        }
    });
})