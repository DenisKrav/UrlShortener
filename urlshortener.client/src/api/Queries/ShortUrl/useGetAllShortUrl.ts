import { useQuery } from '@tanstack/react-query';
import type { ShortUrlModel } from '../../Models/ShortUrl/ShortUrlModel';
import { getAllShortUrls } from '../../Services/ShortUrlService';

export const useGetAllShortUrls = () => {
  return useQuery<ShortUrlModel[], Error>({
    queryKey: ['shortUrls'],
    queryFn: getAllShortUrls,
  });
};