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

  export interface User{
    username : string;
    token : string;
    photoUrl? : string
}
  