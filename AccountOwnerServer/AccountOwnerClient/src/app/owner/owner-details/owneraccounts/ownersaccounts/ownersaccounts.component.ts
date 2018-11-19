import { Component, OnInit, Input } from '@angular/core';

@Component({
  selector: 'app-ownersaccounts',
  templateUrl: './ownersaccounts.component.html',
  styleUrls: ['./ownersaccounts.component.css']
})
export class OwnersaccountsComponent implements OnInit {
  @Input() public accounts: Account[] = [];
  constructor() { }

  ngOnInit() {
  }

}
