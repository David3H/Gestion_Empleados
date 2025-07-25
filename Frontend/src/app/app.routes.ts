import { Routes } from '@angular/router';
import { EmployeeListComponent } from './employees/list/employee-list/employee-list.component';
import { EmployeeFormComponent } from './employees/form/employee-form/employee-form.component';
import { LoginComponent } from './login/login/login.component';
import { authGuard } from './core/guards/auth.guard';

export const routes: Routes = [
  { path: 'login', component: LoginComponent, pathMatch: 'full' },
  { path: '', canActivate: [authGuard], children: [
      { path: '', component: EmployeeListComponent,pathMatch: 'full' },
      { path: 'create', component: EmployeeFormComponent },
      { path: 'edit/:id', component: EmployeeFormComponent }
    ]
  },
  { path: '**', redirectTo: ''}
];
