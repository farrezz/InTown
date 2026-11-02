

// matches EventDto.cs

export interface EventDto{
  id: number;
  titel: string;
  location: string;
  date: string;
  imageUrl: string;
  createdByName: string;
  attendeeCount: number;
}


// "extends EventDto" = the same as ": EventDto" in C#
export interface EventDetailDto extends EventDto {
  description: string;
  createdByUserId: number;
  attendeeNames: string[]; // List<string> in C#
}

// matches CreateEventRequest.cs, used for both create and edit
export interface CreateEventRequest {
  title: string;
  location: string;
  description: string;
  date: string;
}
