export interface Member {
  id: number
  username: string
  age: number
  photoUrl: string
  knownAs: string
  created: Date
  lastActive: Date
  gender: string
  introduction: string
  interests: any
  lookingFor: string
  city: string
  country: string
  photos: Photo[]
}

export interface Photo {
  id: number
  url: string
  ismain: boolean
  publicId: any
}

export interface User {
  username: string;
  knownAs: string
  gender: string
  token: string;
  photoUrl?: string
}

export interface Pagination {
  currentPage: number;
  itemsPerPage: number;
  totalItems: number
  totalPages: number
}

export class PaginatedResult<T> {
  items?: T
  pagination?: Pagination
}

export class UserParams {
  gender: string
  minAge = 18
  maxAge = 99
  pageNumber = 1
  pageSize = 5
  orderBy = 'lastActive'

  constructor(user: User | null) {
    this.gender = user?.gender === 'female' ? 'male' : 'female'


  }

}
