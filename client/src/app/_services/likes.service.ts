import { inject, Injectable, signal } from '@angular/core';
import { environment } from '../../environments/environment';
import { HttpClient } from '@angular/common/http';
import { Member, PaginatedResult } from '../_models/models';
import { setPaginatedResponse, setPaginationHeaders } from './paginationHelper';

@Injectable({
  providedIn: 'root'
})
export class LikesService {
  baseUrl = environment.apiUrl
  private http = inject(HttpClient);
  likeIds = signal<number[]>([]);
  paginatedResult = signal<PaginatedResult<Member[]> | null>(null)

  toggleLike(targetId: number) {
    return this.http.post(`${this.baseUrl}likes/${targetId}`, {})
  }

  getLikes(predicate: string, pageNumber: number, pageSize: number) {
let params = setPaginationHeaders(pageNumber,pageSize)
params = params.append('predicate', predicate)


    return this.http.get<Member[]>(`${this.baseUrl}likes`,{observe: 'response', params}).subscribe({
      next: response => setPaginatedResponse(response, this.paginatedResult)
    })
  }

  getLikesIds() {
    return this.http.get<number[]>(`${this.baseUrl}likes/list`).subscribe({
      next: response => this.likeIds.set(response)
    })
  }

}
