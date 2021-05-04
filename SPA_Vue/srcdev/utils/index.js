const fancytree = {
    init: function(element){
      fancytree.loadTree(element);
      fancytree.registerEvent();
    },
    registerEvent: function(){

        $('.fancy-collapse').off('click').on('click', function () {
            $("#treetable").fancytree("getTree").expandAll(false);
        });
        $('.fancy-expand').off('click').on('click', function () {
            $("#treetable").fancytree("getTree").expandAll();
        });
    },
    loadTree: function(element){
        var CLIPBOARD = null;
        var glyph_opts = {
            preset: "bootstrap3",
            map: {
            }
        };
        $.ui.fancytree.debugLevel = 3; // silence debug output

        function logEvent(event, data, msg) {
            //        var args = $.isArray(args) ? args.join(", ") :
            msg = msg ? ": " + msg : "";
            $.ui.fancytree.info("Event('" + event.type + "', node=" + data.node + ")" + msg);
        }
       console.log(element);
       debugger
       let tree = $(element).fancytree({
            extensions: ["glyph", "table"],
            checkbox: false,
            selectMode: 2,
            dnd5: {
                preventVoidMoves: true,
                preventRecursion: true,
                autoExpandMS: 400,
                dragStart: function (node, data) {
                    return true;
                },
                dragEnter: function (node, data) {
                    // return ["before", "after"];
                    return true;
                },
                dragDrop: function (node, data) {
                    data.otherNode.moveTo(node, data.hitMode);
                }
            },
            glyph: glyph_opts,
            source: { url: "https://localhost:44309/AdminKPILevel/GetListTree", debugDelay: 1000 },
            table: {
                indentation: 20,
                nodeColumnIdx: 1,
                //checkboxColumnIdx: 0
            },
            gridnav: {
                autofocusInput: false,
                handleCursorKeys: true
            },
            focus: function (event, data) {
                event.preventDefault();
                logEvent(event, data, ", targetType=" + data.targetType);

                // $('#box .kpi-name h3').text('KPI - ' + data.node.title);
                // $('#box .kpi-name .code').text(data.node.key);

                $('html, body').animate({
                    scrollTop: $("#box").offset().top
                }, 500)
                // return false to prevent default behavior (i.e. activation, ...)
                //return false;
            },
            lazyLoad: function (event, data) {
                //data.result = { url: "/GetListTree", debugDelay: 1000 };
            },
            renderColumns: function (event, data) {
                var node = data.node,
                    $tdList = $(node.tr).find(">td");

                // (Index #0 is rendered by fancytree by adding the checkbox)
                // Set column #1 info from node data:
                // (Index #2 is rendered by fancytree)
                // Set column #3 info from node data:

                $tdList.eq(0).addClass('text-bold').text(node.data.levelnumber);
                $tdList.eq(1).find('span.fancytree-icon').removeClass('fancytree-icon').addClass('fa fa-book')
                $tdList.eq(1).addClass('text-bold');
                // Static markup (more efficiently defined as html row template):
                // $tdList.eq(3).html("<input type='input' value='" + "" + "'>");
                // ...
            }
        });
      
    }
}