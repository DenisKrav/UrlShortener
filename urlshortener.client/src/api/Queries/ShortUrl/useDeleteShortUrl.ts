import { useMutation } from '@tanstack/react-query';
import { deleteShortUrl } from '../../Services/ShortUrlService';
import type { DeleteShortUrlModel } from '../../Models/ShortUrl/DeleteShortUrlModel';

export const useDeleteShortUrl = (token: string) => {
  return useMutation<boolean, Error, DeleteShortUrlModel>({
    mutationFn: (payload) => deleteShortUrl(payload, token),
  });
};
