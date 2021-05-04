 getAll() {
      var glyph_opts = {
        preset: "bootstrap3",
        map: {}
      };
      $(function() {
        // Initialize Fancytree
        $("#tree").fancytree({
          extensions: ["dnd5", "edit", "glyph", "wide"],
          checkbox: true,
          selectMode: 3,
          dnd5: {
            dragStart: function(node, data) {
              return true;
            },
            dragEnter: function(node, data) {
              return true;
            },
            dragDrop: function(node, data) {
              data.otherNode.copyTo(node, data.hitMode);
            }
          },
          glyph: glyph_opts,
          // source: {url: "ajax-tree-taxonomy.json", debugDelay: 1000},
          source: { url: "https://localhost:44309/AdminKPILevel/GetListTree", debugDelay: 1000 },
          // toggleEffect: { effect: "drop", options: {direction: "left"}, duration: 400 },
          wide: {
            iconWidth: "1em", // Adjust this if @fancy-icon-width != "16px"
            iconSpacing: "0.5em", // Adjust this if @fancy-icon-spacing != "3px"
            labelSpacing: "0.1em", // Adjust this if padding between icon and label != "3px"
            levelOfs: "1.5em" // Adjust this if ul padding != "16px"
          },

          icon: function(event, data) {
            // if( data.node.isFolder() ) {
            //   return "glyphicon glyphicon-book";
            // }
          },
          lazyLoad: function(event, data) {
            data.result = { url: "ajax-sub2.json", debugDelay: 1000 };
          }
        });
      });
    }