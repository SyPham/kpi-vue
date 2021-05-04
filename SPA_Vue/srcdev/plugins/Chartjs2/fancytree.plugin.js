let glyph_opts = {
  preset: "bootstrap3",
  map: {}
};
function logEvent(event, data, msg) {
  // var args = $.isArray(args) ? args.join(", ") :
  msg = msg ? ": " + msg : "";
  $.ui.fancytree.info(
    "Event('" + event.type + "', node=" + data.node + ")" + msg
  );
}
 const fancytree = (url,focus) => {
  
  $("#treetable").fancytree({
    extensions: ["glyph", "table"],
    checkbox: false,
    selectMode: 2,
    dnd5: {
      preventVoidMoves: true,
      preventRecursion: true,
      autoExpandMS: 400,
      dragStart: function(node, data) {
        return true;
      },
      dragEnter: function(node, data) {
        return true;
      },
      dragDrop: function(node, data) {
        data.otherNode.moveTo(node, data.hitMode);
      }
    },
    glyph: glyph_opts,
    source: {
      url: url
    },
    table: {
      indentation: 20,
      nodeColumnIdx: 1
    },
    gridnav: {
      autofocusInput: false,
      handleCursorKeys: true
    },
    focus: focus,
    renderColumns: function(event, data) {
      var node = data.node,
        $tdList = $(node.tr).find(">td");
      $tdList
        .eq(0)
        .addClass("text-bold")
        .text(node.data.levelnumber);
      $tdList
        .eq(1)
        .find("span.fancytree-icon")
        .removeClass("fancytree-icon")
        .addClass("fa fa-book");
      $tdList.eq(1).addClass("text-bold");
      $tdList.eq(1).addClass("text-bold");
    }
  });
}

export { fancytree }
// export const fancytree =  $("#treetable").fancytree({
//   extensions: ["glyph", "table"],
//   checkbox: false,
//   selectMode: 2,
//   dnd5: {
//     preventVoidMoves: true,
//     preventRecursion: true,
//     autoExpandMS: 400,
//     dragStart: function(node, data) {
//       return true;
//     },
//     dragEnter: function(node, data) {
//       return true;
//     },
//     dragDrop: function(node, data) {
//       data.otherNode.moveTo(node, data.hitMode);
//     }
//   },
//   glyph: glyph_opts,
//   source: {
//     url: "http://10.4.4.224:98/AdminKPILevel/GetListTree"
//   },
//   table: {
//     indentation: 20,
//     nodeColumnIdx: 1
//   },
//   gridnav: {
//     autofocusInput: false,
//     handleCursorKeys: true
//   },
//   focus: function(event, data) {
//     logEvent(event, data, ", targetType=" + data.targetType);
//     var node = data.node,
//       $tdList = $(node.tr).find(">td");
//     var level = Number($tdList.eq(0).text());
//     var name = $tdList.eq(1).text();
//     $("#boxCategory .kpi-name h3").text("Category list - " + name);
//     $("#boxCategory .levelID").val(data.node.key);
//     $("#box .catid").val("");
//     $("#box .kpi-name h3").text("KPI");
//     $("#box").fadeOut();
//     $("#box .periodAll").hide();

//     categoryKPILevelAdmin.getAllCategories(true, level, data.node.key);
//     categoryKPILevelAdmin.getAllKPILevelByCategory(true, 0);
//   },
//   renderColumns: function(event, data) {
//     var node = data.node,
//       $tdList = $(node.tr).find(">td");
//     $tdList
//       .eq(0)
//       .addClass("text-bold")
//       .text(node.data.levelnumber);
//     $tdList
//       .eq(1)
//       .find("span.fancytree-icon")
//       .removeClass("fancytree-icon")
//       .addClass("fa fa-book");
//     $tdList.eq(1).addClass("text-bold");
//     $tdList.eq(1).addClass("text-bold");
//   }
// });

