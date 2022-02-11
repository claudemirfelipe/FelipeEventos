import { Component, EventEmitter, OnInit, Output } from '@angular/core';
import { AbstractControlOptions, FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { ValidatorField } from '@app/helpers/ValidatorField';
import { UserUpdate } from '@app/models/identity/UserUpdate';
import { AccountService } from '@app/services/account.service';
import { PalestranteService } from '@app/services/palestrante.service';
import { NgxSpinnerService } from 'ngx-spinner';
import { ToastrService } from 'ngx-toastr';


@Component({
  selector: 'app-perfil-detalhe',
  templateUrl: './perfil-detalhe.component.html',
  styleUrls: ['./perfil-detalhe.component.scss']
})
export class PerfilDetalheComponent implements OnInit {

  @Output() changeFormValue = new EventEmitter();

  userUpdate = {} as UserUpdate;
  form: FormGroup;
  public imagemURL = '';

  constructor(private fb: FormBuilder,
              public accountService: AccountService,
              public palestranteService: PalestranteService,
              private router: Router,
              private toaster: ToastrService,
              private spinner: NgxSpinnerService) { }

  ngOnInit() {
    this.validation();
    this.carregarUsuario();
    this.verificaForm();
  }

  private verificaForm(): void {
    this.form.valueChanges.subscribe(() => this.changeFormValue.emit({...this.form.value}))
  }

  private carregarUsuario(): void {
    this.spinner.show();
    this.accountService
      .getUser()
      .subscribe(
      (userRetorno: UserUpdate) => {
        console.log(userRetorno);
        this.userUpdate = userRetorno;
        this.form.patchValue(this.userUpdate);
        // this.toaster.success('Usuário Carregado', 'Sucesso!');
      },
      (error) => {
        console.error(error);
        this.toaster.error('Usuário não Carregado', 'Error!');
        this.router.navigate(['/dashboard'])
      },
    ).add(() => this.spinner.hide())
  }

     // Validação de formulario
  private validation(): void {

    const formOptions: AbstractControlOptions  = {
      validators: ValidatorField.MustMach('password', 'confirmePassword')
    };

    this.form = this.fb.group({
      userName: [''],
      imagemURL: [''],
      titulo: ['NaoInformado', Validators.required],
      primeiroNome: ['', Validators.required],
      ultimoNome: ['', Validators.required],
      email: ['',
        [Validators.required, Validators.email]
      ],
      phoneNumber: ['', Validators.required],
      funcao: ['NaoInformado', Validators.required],
      descricao: ['', Validators.required],
      password: ['',
       [Validators.required, Validators.minLength(4)]
      ],
      confirmePassword: ['', Validators.required],
    }, formOptions);
  }

    // Para Pegar um FormField apenas com a letra F
    get f(): any{ return this.form.controls; }

    onSubmit(): void {
      this.atualizarUsuario();
    }

    public atualizarUsuario() {
      this.userUpdate = { ... this.form.value }

      if (this.f.funcao.value == 'Palestrante') {
        this.palestranteService.post().subscribe(
          () => this.toaster.success('Função atualizada!', 'Sucesso!'),
          (error) => {
            this.toaster.error(error.error, 'Error')
          }
        )
      }

      this.accountService.updateUser(this.userUpdate).subscribe(
        () => this.toaster.success('Usuário atualizado', 'Sucesso!'),
        (error) => {
          this.toaster.error(error.error);
          console.log(error);
        },
      ).add(() => this.spinner.hide())
    }
    // Ira resetar o formulario
    public resetForm(event: any): void {
      event.preventDafault();
      this.form.reset();
    }
}
