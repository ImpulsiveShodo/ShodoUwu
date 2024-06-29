import { _decorator, CharacterController, Component, input, Input, EventKeyboard, KeyCode, Node, v3 } from 'cc';
const { ccclass, property } = _decorator;

@ccclass('CatController')
export class CatController extends Component {
    movement = v3(0, 0, 0);
    characterController: CharacterController | null = null;
    onLoad() {
        input.on(Input.EventType.KEY_DOWN, this.onKeyDown, this);
        input.on(Input.EventType.KEY_UP, this.onKeyUp, this);
    }

    onDestroy() {
        input.off(Input.EventType.KEY_DOWN, this.onKeyDown, this);
        input.off(Input.EventType.KEY_UP, this.onKeyUp, this);
    }

    onKeyDown(event: EventKeyboard) {
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

    onKeyUp(event: EventKeyboard) {
    }
    start() {
        this.characterController = this.node.getComponent(CharacterController);

    }

    update(deltaTime: number) {
        if (this.movement.length() === 0) {
            return;

        }
        this.characterController?.move(this.movement);
        this.movement = v3(0, 0, 0);
    }
}
