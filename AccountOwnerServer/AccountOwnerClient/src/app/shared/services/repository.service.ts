import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { EnviromentUrlService } from './enviroment-url.service';

@Injectable({
  providedIn: 'root',
})
export class RepositoryService {

  constructor(private http: HttpClient, private envUrl: EnviromentUrlService) { }

  public getData(route: string) {
    return this.http.get(this.createCompleteRoute(route, this.envUrl.urlAddress));
  }

  public create(route: string, body) {
    return this.http.post(this.createCompleteRoute(route, this.envUrl.urlAddress), body);
  }

  public update(route: string, body) {
    return this.http.put(this.createCompleteRoute(route, this.envUrl.urlAddress), body);
  }

  public delete(route: string) {
    return this.http.delete(this.createCompleteRoute(route, this.envUrl.urlAddress));
  }

  private createCompleteRoute(route: string, envAddress: string) {
    return `${envAddress}/${route}`;
  }

  private generateHeader() {
    return {
      headers: new HttpHeaders({'Content-Type': 'application/json'})
    };
  }
}
