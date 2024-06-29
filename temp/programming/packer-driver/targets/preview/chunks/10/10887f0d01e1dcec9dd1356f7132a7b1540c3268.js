System.register(["cc"], function (_export, _context) {
  "use strict";

  var _cclegacy, __checkObsolete__, __checkObsoleteInNamespace__, _decorator, CharacterController, Component, input, Input, KeyCode, v3, _dec, _class, _crd, ccclass, property, CatController;

  return {
    setters: [function (_cc) {
      _cclegacy = _cc.cclegacy;
      __checkObsolete__ = _cc.__checkObsolete__;
      __checkObsoleteInNamespace__ = _cc.__checkObsoleteInNamespace__;
      _decorator = _cc._decorator;
      CharacterController = _cc.CharacterController;
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
        constructor() {
          super(...arguments);
          this.speed = v3(0, 0, 0);
          this.g = 9.8;
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
          switch (event.keyCode) {
            case KeyCode.KEY_A:
              this.speed.z = -1;
              break;

            case KeyCode.KEY_D:
              this.speed.z = 1;
              break;

            case KeyCode.KEY_W:
              this.speed.x = 1;
              break;

            case KeyCode.KEY_S:
              this.speed.x = -1;
              break;
            // case KeyCode.SPACE:
            //     if (this.characterController?.isGrounded) {
            //         this.speed.y = 5;
            //     }
            //     break;
          }
        }

        onKeyUp(event) {
          switch (event.keyCode) {
            case KeyCode.KEY_A:
              this.speed.z = 0;
              break;

            case KeyCode.KEY_D:
              this.speed.z = 0;
              break;

            case KeyCode.KEY_W:
              this.speed.x = 0;
              break;

            case KeyCode.KEY_S:
              this.speed.x = 0;
              break;
          }
        }

        start() {
          this.characterController = this.node.getComponent(CharacterController);
        }

        update(deltaTime) {
          var _this$characterContro;

          var pos = this.characterController.centerWorldPosition; // if (this.characterController?.isGrounded === false) {
          //  this.speed.y -= this.g * deltaTime;
          // }

          var speed = this.speed.clone(); // if (speed.length() == 0) {
          //     return;
          // }

          console.log((_this$characterContro = this.characterController) == null ? void 0 : _this$characterContro.isGrounded, this.name, pos.x, pos.y, pos.z, this.speed.x, this.speed.y, this.speed.z); // this.characterController?.move(speed.multiplyScalar(deltaTime));
        }

      }) || _class));

      _cclegacy._RF.pop();

      _crd = false;
    }
  };
});
//# sourceMappingURL=10887f0d01e1dcec9dd1356f7132a7b1540c3268.js.map