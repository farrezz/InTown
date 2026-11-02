import { environment } from "../environments/environment"


// the API stores "/uploads/3f9a1c.jpg", but the file lives on the API's address,
// so we put the API address in front
export function imageUrl(path: string | null): string {
  if (!path) return "";
  return environment.apiUrl + path;
}
