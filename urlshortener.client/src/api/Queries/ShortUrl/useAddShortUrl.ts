import { useMutation } from '@tanstack/react-query';
import type { ShortUrlAddModel } from '../../Models/ShortUrl/ShortUrlAddModel';
import { addShortUrl } from '../../Services/ShortUrlService';
import type { ShortUrlModel } from '../../Models/ShortUrl/ShortUrlModel';

export const useAddShortUrl = (token: string) => {
  return useMutation<ShortUrlModel, Error, ShortUrlAddModel>({
    mutationFn: (urlData) => addShortUrl(urlData, token),
  });
};
