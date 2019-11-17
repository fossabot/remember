using Core;
using Core.Common.Cache;
using Domain;
using Domain.Entities;
using Repositories.Interface;
using Services.Core;
using Services.Interface;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Services.Implement
{
    public partial class RoleInfoService : BaseService<RoleInfo>, IRoleInfoService
    {
        public IList<Sys_Menu> RoleHaveSys_Menus(int roleId)
        {
            IList<Sys_Menu> menuList = new List<Sys_Menu>();
            RoleInfo roleInfo = this.Find(m => m.ID == roleId && !m.IsDeleted);
            if (roleInfo != null)
            {
                var role_menus = roleInfo.Role_Menus;
                if (role_menus != null && role_menus.Count >= 1)
                {
                    var sys_menus = role_menus.Where(m => !m.IsDeleted).Select(m => m.Sys_Menu).ToList();
                    foreach (Sys_Menu menu in sys_menus)
                    {
                        if (!menuList.Contains(menu, new Sys_Menu_Compare()))
                        {
                            menuList.Add(menu);
                        }
                    }
                }
            }

            return menuList;
        }

        public IList<FunctionInfo> RoleHaveFunctions(int roleId)
        {
            IList<FunctionInfo> funcList = new List<FunctionInfo>();
            RoleInfo roleInfo = this.Find(m => m.ID == roleId && !m.IsDeleted);
            if (roleInfo != null)
            {
                var role_funcs = roleInfo.Role_Functions;
                if (role_funcs != null && role_funcs.Count >= 1)
                {
                    var functions = role_funcs.Where(m => !m.IsDeleted).Select(m => m.FunctionInfo).ToList();
                    foreach (FunctionInfo function in functions)
                    {
                        if (!funcList.Contains(function, new FunctionInfo_Compare()))
                        {
                            funcList.Add(function);
                        }
                    }
                }
            }

            return funcList;
        }

        /// <summary>
        /// ĳ��Ϊָ����ɫ����˵��Լ�Ȩ��
        /// </summary>
        /// <param name="roleId">ָ����ɫ</param>
        /// <param name="menuIdList">ӵ�в˵���ID�б�</param>
        /// <param name="funcIdList">ӵ��Ȩ�޵�ID�б�</param>
        /// <param name="operatorId">������/��Ȩ��</param>
        /// <returns></returns>
        public bool AssignPower(int roleId, IList<int> menuIdList, IList<int> funcIdList, int operatorId)
        {
            bool isSuccess = false;
            RoleInfo roleInfo = this.Find(m => m.ID == roleId && !m.IsDeleted);
            if (roleInfo != null)
            {
                // ������ �� �˵��Լ� Ȩ�޲���
                var old_Role_Menus = roleInfo.Role_Menus.Where(m => !m.IsDeleted).ToList();
                var old_Role_Funcs = roleInfo.Role_Functions.Where(m => !m.IsDeleted).ToList();

                try
                {

                    #region �¾ɲ˵�����
                    IList<int> old_Menus_Ids = new List<int>();
                    if (old_Role_Menus != null && old_Role_Menus.Count >= 1)
                    {
                        old_Menus_Ids = old_Role_Menus.Select(m => m.Sys_MenuId).ToList();
                    }

                    IList<int> needDelete_Menus_Ids = new List<int>();
                    // �µ���û�еľ�ɾ��
                    foreach (var item in old_Menus_Ids)
                    {
                        if (!menuIdList.Contains(item))
                        {
                            // �µ�û�� ����ɵģ�ɾ������
                            needDelete_Menus_Ids.Add(item);
                        }
                    }
                    IList<int> needAdd_Menus_Ids = new List<int>();
                    // �ɵ���û�д����µľ�����
                    foreach (var item in menuIdList)
                    {
                        if (!old_Menus_Ids.Contains(item))
                        {
                            // �ɵ�û�д����µģ���������
                            needAdd_Menus_Ids.Add(item);
                        }
                    }

                    // ɾ��
                    IRole_MenuRepository role_MenuRepository = ContainerManager.Resolve<IRole_MenuRepository>();
                    IList<Role_Menu> needDelete_Role_Menus = role_MenuRepository.Filter(m => m.RoleInfoId == roleId && needDelete_Menus_Ids.Contains(m.Sys_MenuId) && !m.IsDeleted).ToList();
                    foreach (var item in needDelete_Role_Menus)
                    {
                        item.DeletedAt = DateTime.Now;
                        item.IsDeleted = true;
                        role_MenuRepository.Update(item);
                    }
                    // ����
                    foreach (var item in needAdd_Menus_Ids)
                    {
                        role_MenuRepository.Create(new Role_Menu
                        {
                            RoleInfoId = roleId,
                            Sys_MenuId = item,
                            CreateTime = DateTime.Now,
                            OperatorId = operatorId
                        });
                    }
                    // ͳһ����
                    role_MenuRepository.SaveChanges();
                    #endregion


                    #region �¾�Ȩ�޲���
                    IList<int> old_Funcs_Ids = new List<int>();
                    if (old_Role_Funcs != null && old_Role_Funcs.Count >= 1)
                    {
                        old_Funcs_Ids = old_Role_Funcs.Select(m => m.FunctionInfoId).ToList();
                    }

                    IList<int> needDelete_Funcs_Ids = new List<int>();
                    // �µ���û�еľ�ɾ��
                    foreach (var item in old_Funcs_Ids)
                    {
                        if (!funcIdList.Contains(item))
                        {
                            // �µ�û�� ����ɵģ�ɾ������
                            needDelete_Funcs_Ids.Add(item);
                        }
                    }
                    IList<int> needAdd_Funcs_Ids = new List<int>();
                    // �ɵ���û�д����µľ�����
                    foreach (var item in funcIdList)
                    {
                        if (!old_Funcs_Ids.Contains(item))
                        {
                            // �ɵ�û�д����µģ���������
                            needAdd_Funcs_Ids.Add(item);
                        }
                    }

                    // ɾ��
                    IRole_FunctionRepository role_FuncRepository = ContainerManager.Resolve<IRole_FunctionRepository>();
                    IList<Role_Function> needDelete_Role_Funcs = role_FuncRepository.Filter(m => m.RoleInfoId == roleId && needDelete_Funcs_Ids.Contains(m.FunctionInfoId) && !m.IsDeleted).ToList();
                    foreach (var item in needDelete_Role_Funcs)
                    {
                        item.DeletedAt = DateTime.Now;
                        item.IsDeleted = true;
                        role_FuncRepository.Update(item);
                    }
                    // ����
                    foreach (var item in needAdd_Funcs_Ids)
                    {
                        role_FuncRepository.Create(new Role_Function
                        {
                            RoleInfoId = roleId,
                            FunctionInfoId = item,
                            CreateTime = DateTime.Now,
                            OperatorId = operatorId
                        });
                    }
                    // ͳһ����
                    role_FuncRepository.SaveChanges();
                    #endregion

                    isSuccess = true;

                    // ˢ����ػ���
                    // ʧ�ܣ���Ч�������������ʣ�̫���û�
                    //var userInfoList = roleInfo.UserInfos;
                    //if (userInfoList != null && userInfoList.Count >= 1)
                    //{
                    //    int[] userIds = userInfoList.Select(m => m.ID).ToArray();
                    //    foreach (var userId in userIds)
                    //    {
                    //        CacheHelper.Remove($"UserHaveAuthKeys({userId})");
                    //    }
                    //}
                }
                catch (Exception ex)
                {
                    isSuccess = false;
                }

            }

            return isSuccess;
        }
    }
}
