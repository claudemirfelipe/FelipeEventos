import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';

import { CollapseModule } from 'ngx-bootstrap/collapse';
import { TooltipModule} from 'ngx-bootstrap/tooltip';
import { BsDropdownModule } from 'ngx-bootstrap/dropdown';
import { ModalModule } from 'ngx-bootstrap/modal';
import { BsDatepickerModule } from 'ngx-bootstrap/datepicker';
import { defineLocale } from 'ngx-bootstrap/chronos';
import { ptBrLocale } from 'ngx-bootstrap/locale';
import { NgxCurrencyModule } from 'ngx-currency';
import { PaginationModule } from 'ngx-bootstrap/pagination';
import { TabsModule } from 'ngx-bootstrap/tabs';

import { ToastrModule } from 'ngx-toastr';
import { NgxSpinnerModule } from 'ngx-spinner';

import { AppRoutingModule } from '@app/app-routing.module';
import { AppComponent } from '@app/app.component';

import { ContatosComponent } from '@app/components/contatos/contatos.component';
import { DashboardComponent } from '@app/components/dashboard/dashboard.component';

import { EventosComponent } from '@app/components/eventos/eventos.component';
import { EventoDetalheComponent } from '@app/components/eventos/evento-detalhe/evento-detalhe.component';
import { EventoListaComponent } from '@app/components/eventos/evento-lista/evento-lista.component';

import { PalestrantesComponent } from '@app/components/palestrantes/palestrantes.component';
import { PerfilComponent } from '@app/components/user/perfil/perfil.component';

import { NavegacaoComponent } from '@app/shared/navegacao/navegacao.component';
import { NotFoundComponent } from '@app/shared/navegacao/not-found/not-found/not-found.component';
import { TituloComponent } from '@app/shared/titulo/titulo.component';

import { HomeComponent } from '@app/components/home/home.component';

import { EventoService } from '@app/services/evento.service';
import { LoteService } from '@app/services/lote.service';
import { DateTimeFormatPipe } from '@app/helpers/DateTimeFormat.pipe';
import { UserComponent } from '@app/components/user/user.component';
import { LoginComponent } from '@app/components/user/login/login.component';
import { RegistrationComponent } from '@app/components/user/registration/registration.component';
import { AccountService } from '@app/services/account.service';
import { JwtInterceptor } from '@app/interceptors/jwt.interceptor';
import { PerfilDetalheComponent } from './components/user/perfil/perfil-detalhe/perfil-detalhe.component';
import { PalestranteService } from './services/palestrante.service';
import { PalestranteListaComponent } from './components/palestrantes/palestrante-lista/palestrante-lista.component';
import { PalestranteDetalheComponent } from './components/palestrantes/palestrante-detalhe/palestrante-detalhe.component';
import { RedesSociaisComponent } from './components/redesSociais/redesSociais.component';
import { RedeSocialService } from './services/redeSocial.service.service';


defineLocale('pt-br', ptBrLocale);
@NgModule({
  declarations: [
    AppComponent,
    ContatosComponent,
    DashboardComponent,
    EventosComponent,
    PalestrantesComponent,
    PalestranteListaComponent,
    PalestranteDetalheComponent,
    PerfilComponent,
    PerfilDetalheComponent,
    RedesSociaisComponent,
    NavegacaoComponent,
    TituloComponent,
    NotFoundComponent,
    DateTimeFormatPipe,
    EventoDetalheComponent,
    EventoListaComponent,
    UserComponent,
    LoginComponent,
    RegistrationComponent,
    HomeComponent,
   ],
  imports: [
    BrowserModule,
    FormsModule,
    ReactiveFormsModule,
    AppRoutingModule,
    HttpClientModule,
    BrowserAnimationsModule,
    CollapseModule.forRoot(),
    TooltipModule.forRoot(),
    BsDropdownModule.forRoot(),
    ModalModule.forRoot(),
    ToastrModule.forRoot({
      timeOut: 5000,
      positionClass: 'toast-top-right',
      preventDuplicates: true,
      progressBar: true,
    }),
    NgxSpinnerModule,
    BsDatepickerModule.forRoot(),
    NgxCurrencyModule,
    PaginationModule.forRoot(),
    TabsModule.forRoot(),
  ],
  providers: [AccountService,
              EventoService,
              LoteService,
              PalestranteService,
              RedeSocialService,
              { provide: HTTP_INTERCEPTORS, useClass: JwtInterceptor, multi: true},
  ],
  bootstrap: [AppComponent],
})
export class AppModule { }
