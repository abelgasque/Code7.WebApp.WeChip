import { Component } from '@angular/core';
import { MenuItem } from 'primeng/components/common/menuitem';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html'
})
export class AppComponent {
  
  title = 'app';
  items: MenuItem[];

  constructor(){
    this.getMenu();
  }

  getMenu(){
    this.items = [
      {
        label: 'Menu',
        icon: 'pi pi-bars',
        items: [
          {
            label: 'Home',
            icon: 'pi pi-home',
            routerLink: '/'
          },
          {
            label: 'Clientes',
            icon: 'pi pi-users',
            routerLink: '/cliente'
          },
          {
            label: 'Ofertas',
            icon: 'pi pi-shopping-cart',
            routerLink: '/oferta'
          }
        ],
      }
   ];
  }
}
