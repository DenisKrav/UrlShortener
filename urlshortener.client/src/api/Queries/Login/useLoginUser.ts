import { useMutation } from '@tanstack/react-query';
import { loginUser } from '../../Services/LoginService';
import type { LoginModel } from '../../Models/Login/LoginModel';

export const useLoginUser = () => {
  return useMutation<string, Error, LoginModel>({
    mutationFn: loginUser,
  });
};
