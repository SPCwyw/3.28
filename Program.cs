using System;
//请使用委托实现信用卡用户定时还款功能
namespace LeeCodeText1
{
	//信用卡类
	class CreditCard
	{
		//还款金额
		private int money;
		//还款日
		private string repaymentDate;
		//还款金额的get，set方法
		public void setMoney()
		{
			Console.Write("请输入本月需还款的金额:");
			int money = Convert.ToInt32(Console.ReadLine());
			this.money = money;
		}
		public int getMOney()
		{
			return money;
		}
		//还款日的get，set方法
		public void setRepaymentDate()
		{
			Console.Write("还款日为:");
			string repaymentDate = Console.ReadLine();
			this.repaymentDate = repaymentDate;
		}
		public string getRepayment()
		{
			return repaymentDate;
		}
		//还款日还款方法
		public void repayment(Bank bank, BankCard bankCard)
		{
			Console.WriteLine("今天是{0},还款日已到，此次需还款金额为 :{1}元，请还款。", this.repaymentDate, this.money);
			int balance = bankCard.getBalance();
			balance -= this.money;
			bankCard.setBalance(balance);
			while (balance < 0)
			{
				Console.WriteLine("对不起，当前余额为：{0}，余额不足，请充值！！！", balance);
				bankCard.setBalance(bank);
				balance = bankCard.getBalance();
			}
			Console.WriteLine("恭喜您，还款成功，余额还剩：{0}", balance);
		}
	}
	//银行卡类
	class BankCard
	{
		//银行卡余额
		private int balance;
		//银行卡余额的get，set方法
		public void setBalance(Bank bank)
		{
			int add = bank.add();
			this.balance += add;
		}
		public void setBalance(int banlance)
		{
			this.balance = banlance;

		}
		public int getBalance()
		{
			return balance;
		}
	}
	//银行类
	class Bank
	{
		//充值余额
		public int add()
		{
			Console.Write("请输入充值金额:");
			int addMoney = Convert.ToInt32(Console.ReadLine());
			Console.WriteLine("恭喜您，成功缴纳人民币{0}元，欢迎下次光临！", addMoney);
			return addMoney;
		}
	}

	class AutomaticPayment
	{
		//定义委托
		public delegate void delegateAutomaticPayment(Bank bank, BankCard bancard);
		//定义事件
		public event delegateAutomaticPayment eventAutomaticPayment;

		//调用事件委托
		public void auto(Bank bank, BankCard bankCard)
		{
			if (eventAutomaticPayment != null)
			{
				eventAutomaticPayment(bank, bankCard);
			}

		}
	}


	class Program
	{
		static void Main(string[] args)
		{
			//委托对象
			AutomaticPayment objDelegate = new AutomaticPayment();
			//银行实例对象
			Bank bank = new Bank();
			//银行卡实例对象
			BankCard bankCard = new BankCard();
			//信用卡实例对象
			CreditCard creditCard = new CreditCard();
			//设置银行卡本钱
			//bankCard.setBalance(bank);
			//设置还款金额
			creditCard.setMoney();
			//设置还款日期
			creditCard.setRepaymentDate();
			//订阅事件,还款方法
			objDelegate.eventAutomaticPayment += new AutomaticPayment.delegateAutomaticPayment(creditCard.repayment);
			objDelegate.auto(bank, bankCard);
		}
	}
}
