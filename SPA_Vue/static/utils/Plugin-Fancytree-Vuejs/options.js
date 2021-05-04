const logEvent = function (event, data, msg) {
  // var args = $.isArray(args) ? args.join(", ") :
  msg = msg ? ": " + msg : "";
  $.ui.fancytree.info(
    "Event('" + event.type + "', node=" + data.node + ")" + msg
  );
}
const dnd5 = {
  preventVoidMoves: true,
  preventRecursion: true,
  autoExpandMS: 400,
  dragStart: function(node, data) {
    return true;
  },
  dragEnter: function(node, data) {
    // return ["before", "after"];
    return true;
  },
  dragDrop: function(node, data) {
    data.otherNode.moveTo(node, data.hitMode);
  }
};

const table = {
  indentation: 20,
  nodeColumnIdx: 1
  //checkboxColumnIdx: 0
};

const gridnav = {
  autofocusInput: false,
  handleCursorKeys: true
};
const renderColumns = function(event, data) {
  var node = data.node,
    $tdList = $(node.tr).find(">td");

  // (Index #0 is rendered by fancytree by adding the checkbox)
  // Set column #1 info from node data:
  // (Index #2 is rendered by fancytree)
  // Set column #3 info from node data:

  $tdList
    .eq(0)
    .addClass("text-bold")
    .text(node.data.levelnumber);
  $tdList
    .eq(1)
    .find("span.fancytree-icon")
    .removeClass("fancytree-icon")
    .addClass("fa fa-home");
  $tdList.eq(1).addClass("text-bold");
  // Static markup (more efficiently defined as html row template):
  // $tdList.eq(3).html("<input type='input' value='" + "" + "'>");
  // ...
};
export { dnd5, table, gridnav, renderColumns, logEvent};
