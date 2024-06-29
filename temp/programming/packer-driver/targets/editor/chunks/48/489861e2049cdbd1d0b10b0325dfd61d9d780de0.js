System.register(["cc"], function (_export, _context) {
  "use strict";

  var _cclegacy, __checkObsolete__, __checkObsoleteInNamespace__, _decorator, Component, input, Input, KeyCode, v3, _dec, _class, _crd, ccclass, property, CatController;

  return {
    setters: [function (_cc) {
      _cclegacy = _cc.cclegacy;
      __checkObsolete__ = _cc.__checkObsolete__;
      __checkObsoleteInNamespace__ = _cc.__checkObsoleteInNamespace__;
      _decorator = _cc._decorator;
      Component = _cc.Component;
      input = _cc.input;
      Input = _cc.Input;
      KeyCode = _cc.KeyCode;
      v3 = _cc.v3;
    }],
    execute: function () {
      _crd = true;

      _cclegacy._RF.push({}, "0ddefqeTk5F3a7qa81Q/jLK", "CatController", undefined);

      __checkObsolete__(['_decorator', 'CharacterController', 'Component', 'input', 'Input', 'EventKeyboard', 'KeyCode', 'Node', 'v3']);

      ({
        ccclass,
        property
      } = _decorator);

      _export("CatController", CatController = (_dec = ccclass('CatController'), _dec(_class = class CatController extends Component {
        constructor(...args) {
          super(...args);
          this.movement = v3(0, 0, 0);
          this.characterController = null;
        }

        onLoad() {
          input.on(Input.EventType.KEY_DOWN, this.onKeyDown, this);
          input.on(Input.EventType.KEY_UP, this.onKeyUp, this);
        }

        onDestroy() {
          input.off(Input.EventType.KEY_DOWN, this.onKeyDown, this);
          input.off(Input.EventType.KEY_UP, this.onKeyUp, this);
        }

        onKeyDown(event) {
          console.log(event.keyCode);

          switch (event.keyCode) {
            case KeyCode.KEY_A:
              this.movement.x = -1;
              break;

            case KeyCode.KEY_D:
              this.movement.x = 1;
              break;

            case KeyCode.KEY_W:
              this.movement.z = -1;
              break;

            case KeyCode.KEY_S:
              this.movement.z = 1;
              break;
          }
        }

        onKeyUp(event) {}

        start() {}

        update(deltaTime) {
          var _this$characterContro;

          (_this$characterContro = this.characterController) == null ? void 0 : _this$characterContro.move(this.movement);
          this.movement = v3(0, 0, 0);
        }

      }) || _class));

      _cclegacy._RF.pop();

      _crd = false;
    }
  };
});
//# sourceMappingURL=489861e2049cdbd1d0b10b0325dfd61d9d780de0.js.map