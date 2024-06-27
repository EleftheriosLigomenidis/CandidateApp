
import { FormBuilder, FormGroup, Validators } from "@angular/forms";
import { Base } from "./Base";

export class Degree extends Base {
    name!:string;

    toFormGroup(fb:FormBuilder): FormGroup {
        return fb.group({
            id:[this?.id ],
            name:[this?.name,[Validators.maxLength(500), Validators.required]],
        })
    }

    toModel(obj:any) :Degree {
        this.id = obj.id;
        this.name = obj.name;
        return this;
    }
}
