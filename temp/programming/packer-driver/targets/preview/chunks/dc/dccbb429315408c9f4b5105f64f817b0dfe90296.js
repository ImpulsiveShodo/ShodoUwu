System.register(["cc"], function (_export, _context) {
  "use strict";

  var _cclegacy, __checkObsolete__, __checkObsoleteInNamespace__, _decorator, CharacterController, Component, v3, _dec, _class, _crd, ccclass, property, CatController;

  return {
    setters: [function (_cc) {
      _cclegacy = _cc.cclegacy;
      __checkObsolete__ = _cc.__checkObsolete__;
      __checkObsoleteInNamespace__ = _cc.__checkObsoleteInNamespace__;
      _decorator = _cc._decorator;
      CharacterController = _cc.CharacterController;
      Component = _cc.Component;
      v3 = _cc.v3;
    }],
    execute: function () {
      _crd = true;

      _cclegacy._RF.push({}, "0ddefqeTk5F3a7qa81Q/jLK", "CatController", undefined);

      __checkObsolete__(['_decorator', 'CharacterController', 'Component', 'Node', 'v3']);

      ({
        ccclass,
        property
      } = _decorator);

      _export("CatController", CatController = (_dec = ccclass('CatController'), _dec(_class = class CatController extends Component {
        start() {}

        update(deltaTime) {
          var movement = v3(1.0, 0, 0);
          var characterController = this.node.getComponent(CharacterController);
          characterController == null ? void 0 : characterController.move(movement);
        }

      }) || _class));

      _cclegacy._RF.pop();

      _crd = false;
    }
  };
});
//# sourceMappingURL=dccbb429315408c9f4b5105f64f817b0dfe90296.js.map