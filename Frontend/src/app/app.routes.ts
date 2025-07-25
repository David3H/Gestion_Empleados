import { Routes } from '@angular/router';
import { EmployeeListComponent } from './employees/list/employee-list/employee-list.component';
import { EmployeeFormComponent } from './employees/form/employee-form/employee-form.component';
import { authGuard as EmployeeAuthGuard } from './employees/guards/auth.guard';

export const routes: Routes = [
   { path: '', component: EmployeeListComponent },
  { path: 'create', component: EmployeeFormComponent, canActivate: [EmployeeAuthGuard] },
  { path: 'edit/:id', component: EmployeeFormComponent, canActivate: [EmployeeAuthGuard] },
];
