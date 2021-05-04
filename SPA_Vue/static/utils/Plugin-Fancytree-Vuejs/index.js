import { dnd5, table, gridnav, renderColumns, logEvent } from "./options";
let glyph_opts = {
  preset: "bootstrap3",
  map: {}
};

const initTree = function(url, focus) {
  const tree = $("#treetable").fancytree({
    extensions: ["glyph", "table"],
    checkbox: false,
    selectMode: 2,
    dnd5: dnd5,
    glyph: glyph_opts,
    source: {
      url: url,
      debugDelay: 1000
    },
    table: table,
    gridnav: gridnav,
    focus: focus,
    renderColumns: renderColumns
  });
};

const loadtree = () => {
    $.ui.fancytree.getTree("#treetable").reload().done();
}
export { initTree ,loadtree};
